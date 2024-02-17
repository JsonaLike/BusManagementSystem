using BusManagementSystem.Models;
using BusManagementSystem.Roles;
using BusManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Passenger = BusManagementSystem.Models.Passenger;

namespace BusManagementSystem.Controllers
{
    public class PassengerController : Controller
    {
        private readonly ReservationSystemContext _ReservationSystemContext;
        private readonly ILogger<HomeController> _logger;
        private IPassengerRepository PassengerRepository;
        private ITicketRepository TicketRepository;
        private IGenderMappingRepository GenderMapppingRepository;
        public PassengerController(ReservationSystemContext _ReservationSystemContext, ILogger<HomeController> logger,IPassengerRepository PassengerRepository,IGenderMappingRepository GenderMapppingRepository,ITicketRepository TicketRepository)
        {
            this._ReservationSystemContext = _ReservationSystemContext;
            _logger = logger;
            this.PassengerRepository = PassengerRepository;
            this.GenderMapppingRepository = GenderMapppingRepository;
            this.TicketRepository = TicketRepository;
        }
        public async Task<IActionResult> IndexAsync()
        {
            /*public IEnumerable<Category> AllCategories => _bethanysPieShopDbContext.Categories.OrderBy(p => p.CategoryName);
        var Drivers = _City;*/
                
            var PassengersView = new PassengerViewModel();
            PassengersView.Passengers = PassengerRepository.Passengers;
            PassengersView.GenderMapping = new Dictionary<int, string>();

            /*await GenderMapppingRepository.GenderMapping.ForEachAsync(g => {
                PassengersView.GenderMapping.Add(g.GenderId, g.Gender);
            });*/
            foreach(GenderMapping g in GenderMapppingRepository.GenderMappings)
            {
                PassengersView.GenderMapping.Add(g.GenderId, g.Gender);

            }
            ViewBag.Message = TempData["message"]?.ToString();
            return View(PassengersView);
        }
        public IActionResult AddPassenger()
        {
            var EditView = new EditPassengerViewModel();
            EditView.passenger = new Passenger();
            EditView.Gender = GenderMapppingRepository.GenderMappings;
            return View(EditView);
        }
        [HttpPost]
        public IActionResult AddPassenger(EditPassengerViewModel EditPassengerViewModel)
        {
            if (!IsBirthDateValid(EditPassengerViewModel.passenger.BirthDate))
            {
                TempData["message"] = "<strong>Passenger Age is not Valid.</strong>";
                return RedirectToAction("Index");
            }
            PassengerRepository.Add(EditPassengerViewModel.passenger);
            TempData["message"] = "<strong>Passenger Has been added modified successfully.</strong>";
            return RedirectToAction("Index");
        }
        public IActionResult EditPassenger([FromQuery] int passengerId)
        {
            var EditView = new EditPassengerViewModel();
            EditView.passenger = PassengerRepository.Find(passengerId);
            EditView.Gender = GenderMapppingRepository.GenderMappings;
            return View(EditView);
        }
        [HttpPost]
        public IActionResult EditPassenger(EditPassengerViewModel EditPassengerViewModel)
        {
            if (!IsBirthDateValid(EditPassengerViewModel.passenger.BirthDate))
            {
                TempData["message"] = "<strong>Passenger Age is not Valid.</strong>";
                return RedirectToAction("Index");
            }
            PassengerRepository.Update(EditPassengerViewModel.passenger);
            TempData["message"] = "<strong>Passenger Has been edited successfully.</strong>";
            return RedirectToAction("Index");
        }

        public IActionResult RemovePassenger([FromQuery] int passengerId)
        {
            var passenger = PassengerRepository.Find(passengerId);
            TicketRepository.Tickets
    .Where(ticket => ticket.PassengerId == passenger.PassengerId)
    .ToList()
    .ForEach(ticket => TicketRepository.Delete(ticket));
            PassengerRepository.Delete(PassengerRepository.Find(passengerId));
            TempData["message"] = "<strong>Passenger Has been removed successfully.</strong>";
            return RedirectToAction("Index");
        }
        public bool IsBirthDateValid(DateTime userBirthDate)
        {
            bool isBirthDateValid = true;
                int userAge = DateTime.Now.Year - userBirthDate.Year;
                if (userBirthDate > DateTime.Now || userAge > 100)
                {
                    isBirthDateValid = false;
                }
                else if (userAge < 18)
                {
                    isBirthDateValid = false;
                }
            
            
            return isBirthDateValid;
        }

    }
}
