using BusManagementSystem.Models;
using BusManagementSystem.Roles;
using BusManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Data;
using System.Security.Claims;

namespace BusManagementSystem.Controllers
{
    public class TicketController : Controller
    {
        private readonly ReservationSystemContext _ReservationSystemContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private ITicketRepository TicketRepository;
        private IPassengerRepository PassengerRepository;
        private ITripRepository TripRepository;
        private IBusRepository BusRepository;
        public TicketController(ReservationSystemContext _ReservationSystemContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,ITicketRepository TicketRepository,IPassengerRepository PassengerRepository,ITripRepository TripRepository,IBusRepository BusRepository)
        {
            this._ReservationSystemContext = _ReservationSystemContext;
            _userManager = userManager;
            _roleManager = roleManager;
            this.TicketRepository = TicketRepository;
            this.PassengerRepository = PassengerRepository;
            this.TripRepository = TripRepository;
            this.BusRepository = BusRepository;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddTicket()
        {
            var editTicketViewModel = new EditTicketViewModel();
            editTicketViewModel.Ticket = new Ticket();
            editTicketViewModel.Passengers = PassengerRepository.Passengers;
            editTicketViewModel.Trips = TripRepository.Trips;

            return View(editTicketViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddTicket(EditTicketViewModel editTicketViewModel)
        {
            TripRepository.Find(editTicketViewModel.Ticket.TripId);
            Trip trip = TripRepository.Find(editTicketViewModel.Ticket.TripId);
            Bus bus = BusRepository.Find(trip.BusId);
            var a = 0;
            if (trip.Reserved + 1 > trip.Capacity || trip.Reserved + 1 > bus.Capacity)
            {
                TempData["message"] = "<strong>trip is fully booked and we cannot take any more reservations at this time.</strong>";
                return RedirectToAction("Index");
            }
            try
            {
                TicketRepository.Add(editTicketViewModel.Ticket);
                TempData["message"] = "<strong>Ticket has been added successfully.</strong>";
            }
            catch (InvalidOperationException ex)
            {
                TempData["message"] = "<strong>Passenger is already registered on this trip.</strong>";
            }
            
            return RedirectToAction("Index");
        }
      

        [HttpGet]
        [Authorize(Roles = "Passenger")]
        public async Task<IActionResult> BookTicket([FromQuery] int tripId)
        {
            var user = await _userManager.GetUserAsync(User);
            var passenger =PassengerRepository.Passengers.Single(p => p.user_id == user.Id);
            var trip = TripRepository.Trips.SingleOrDefault(trip => trip.TripId == tripId);
            var bus = trip.Bus;
            if (passenger.Balance - trip.Price < 0)
            {
                TempData["message"] = "<strong>User balance is not enough to book a ticket.</strong>";
                return RedirectToAction("Index");
            }
            else if(trip.Reserved + 1 > trip.Capacity || trip.Reserved + 1 > bus.Capacity)
            {
                TempData["message"] = "<strong>trip is fully booked and we cannot take any more reservations at this time.</strong>";
                return RedirectToAction("Index");
            }
            passenger.Balance = passenger.Balance - trip.Price.Value;
            trip.Reserved += 1;
            TicketRepository.Add(new Ticket() { TripId = tripId,PassengerId= passenger.PassengerId });
            TempData["message"] = "<strong>Ticket has been booked successfully.</strong>";
            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            IdentityUser user=null;
            bool isAuth = User.Identity.IsAuthenticated;
            if (isAuth)
            user = await _userManager.GetUserAsync(User);
            IEnumerable<Ticket> tickets = TicketRepository.Tickets;
            if (isAuth && await _userManager.IsInRoleAsync(user, "Passenger"))
            {
                var passenger = PassengerRepository.Passengers.Single(p => p.user_id == user.Id);

                tickets = tickets.Where(t => t.PassengerId == passenger.PassengerId);
            }
            ViewBag.Message = TempData["message"]?.ToString();
            return View(tickets);
        }
        [Authorize]
        public async Task<IActionResult> RemoveTicket([FromQuery]int tripId,[FromQuery]int passengerId)
        {
            IdentityUser user = null;
            bool isAuth = User.Identity.IsAuthenticated;
            var Ticket = TicketRepository.Find(passengerId,tripId);
            var trip = Ticket.Trip;
            trip.Reserved -= 1;
            var TripDate = trip.Date;
            DateTime currentDate = DateTime.Now;
            TimeSpan difference = TripDate - currentDate;

            if (isAuth)
                user = await _userManager.GetUserAsync(User);
            if(User.FindFirstValue(ClaimTypes.Role) == "Passenger")
            {
                if(difference.TotalDays<= 2)
                {
                    TempData["message"] = "<strong>the ticket Cannot be removed less than two days before the trip.</strong>";
                }
            }
            TicketRepository.Delete(Ticket);
            //refund the user on ticket cancelled
            PassengerRepository.Find(passengerId).Balance = PassengerRepository.Find(passengerId).Balance + TripRepository.Find(tripId).Price.Value;
            TempData["message"] = "<strong>Ticket has been removed successfully.</strong>";
            return RedirectToAction("Index");
        }

       /*[HttpGet("/Ticket/EditTicket/ticketId={ticketId}&passengerId={passengerId}")]
    [Authorize(Roles = "Admin")]
        public IActionResult EditTicket( int ticketId, int passengerId) {
        EditTicketViewModel editTicket = new EditTicketViewModel();
        editTicket.Ticket = _ReservationSystemContext.Tickets.Include(t => t.Passenger)
            .Include(t => t.Trip)
                .ThenInclude(trip => trip.Bus)
            .Include(t => t.Trip)
                .ThenInclude(trip => trip.City)
            .Include(t => t.Trip)
            .ThenInclude(bus => bus.DepartureCityNavigation)
            .Include(t => t.Trip)
            .ThenInclude(bus => bus.ArrivalCityNavigation).First(t => t.TripId == ticketId && t.PassengerId == passengerId);
        editTicket.Cities = _ReservationSystemContext.Cities;
        editTicket.Drivers = _ReservationSystemContext.Drivers;
        editTicket.Buses = _ReservationSystemContext.buses;
        editTicket.Trips = _ReservationSystemContext.Trips
                .Include(t => t.DepartureCityNavigation)
                .Include(t => t.ArrivalCityNavigation);
        editTicket.Passengers = _ReservationSystemContext.Passengers;

            return View(editTicket);
        }*/
    }
}
