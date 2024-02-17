using BusManagementSystem.Models;
using BusManagementSystem.Roles;
using BusManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;

namespace BusManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TripController : Controller
    {
        private readonly ReservationSystemContext _ReservationSystemContext;
        private readonly ILogger<TripController> _logger;
        private ITripRepository TripRepository;
        private IBusRepository BusRepository;
        private ICityRepository CityRepository;
        private IDriverRepository DriverRepository;
        public TripController(ReservationSystemContext reservationSystemContext, ILogger<TripController> logger,ITripRepository TripRepository,IBusRepository BusRepository,ICityRepository CityRepository,IDriverRepository DriverRepository)
        {
            _ReservationSystemContext = reservationSystemContext;
            _logger = logger;
            this.TripRepository = TripRepository;
            this.BusRepository = BusRepository;
            this.CityRepository = CityRepository;
           this.DriverRepository = DriverRepository;
        }

        public IActionResult Index()
        {
            var tripViewModel = new TripViewModel();
          tripViewModel.Trips = TripRepository.Trips;
            tripViewModel.Cities = CityRepository.Cities;
            tripViewModel.Drivers = DriverRepository.Drivers;
            tripViewModel.Buses= BusRepository.Buses;
          
            ViewBag.Message = TempData["message"]?.ToString();
            return View(tripViewModel);
        }
        [HttpGet]
        
        public IActionResult RemoveTrip([FromQuery] int tripId)
        {
            decimal? TripPrice = TripRepository.Find(tripId).Price;
            DateTime TripDate = TripRepository.Find(tripId).Date;

            TripRepository.Delete(TripRepository.Find(tripId));
            if(!(TripDate < DateTime.Now)){ //refund all users if the removed trip is in the future
                _ReservationSystemContext.Tickets.Where(t => t.TripId == tripId).ToList().ForEach(ticket =>
                {
                    var passenger = ticket.Passenger;
                    passenger.Balance += TripPrice.Value;
                });
            }

            _ReservationSystemContext.SaveChanges();
            TempData["message"] = "<strong>Trip has been removed successfully.</strong>";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult AddTrip(EditTripViewModel model)
        {
            model.Trip.Reserved = 0;
            Trip trip =  model.Trip;
            _logger.LogInformation("trip details"+ ModelState.IsValid);
            if(trip.Capacity > BusRepository.Find(trip.BusId).Capacity)
            {
                TempData["message"] = "<strong>Trip cannot have a capacity bigger than bus size.</strong>";
                return RedirectToAction("Index");
            }
            else if(DateTime.Compare(trip.Date, DateTime.Now) <= 0)
            {
                TempData["message"] = "<strong>Trip date must be in the futuer.</strong>";
            }
            TripRepository.Add(trip);
            TempData["message"] = "<strong>Trip has been added successfully.</strong>";
            return RedirectToAction("Index");
        }
        public IActionResult AddTrip() {
            var tripAddViewModel = new EditTripViewModel();
            tripAddViewModel.Trip = new Trip //intialize new trip with default values
            {
                DepartureCity = 1,
                ArrivalCity = 2,
                BusId = 1,
                DriverId = 1,
                StartingTime = TimeSpan.FromHours(0),
                Date = DateTime.Today,
                Capacity = 50,
                Reserved = 0,
                Price = 20.0m
            };
            tripAddViewModel.Cities = CityRepository.Cities;
            tripAddViewModel.Drivers = DriverRepository.Drivers;
            tripAddViewModel.Buses = BusRepository.Buses;
            return View(tripAddViewModel);
        }
        /*public IActionResult TripRemoved() {
            TempData["message"] = "Your trip has been successfully removed!";
            return RedirectToAction("Index");
        }*/
        public IActionResult EditTrip([FromQuery] int tripId) {
            var tripEditViewModel = new EditTripViewModel();
            tripEditViewModel.Trip = TripRepository.Trips.First(t => t.TripId == tripId);
            var trips = TripRepository.Trips;
            tripEditViewModel.Cities = CityRepository.Cities;
            tripEditViewModel.Drivers = DriverRepository.Drivers;
            tripEditViewModel.Buses = BusRepository.Buses;
            
            return View(tripEditViewModel);
        }
        [HttpPost]
        public IActionResult EditTrip(EditTripViewModel model)
        {
            _logger.LogInformation("logging information here"+JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                MaxDepth = 1 // specify the desired level of depth
            }));
            if (DateTime.Compare(model.Trip.Date, DateTime.Now) <= 0)
            {
                TempData["message"] = "<strong>Trip date must be in the future.</strong>";
                return RedirectToAction("Index");
            }
            //Trip trip = _ReservationSystemContext.Trips.Find(model.Trip.TripId);
            TripRepository.Update(model.Trip);
            /*trip.DepartureCity = model.Trip.DepartureCity;
            _logger.LogInformation("Departure City " + trip.DepartureCity);
            trip.ArrivalCity = model.Trip.ArrivalCity;
            _logger.LogInformation("ArrivalCity " + trip.ArrivalCity);
            trip.BusId = model.Trip.BusId;
            trip.DriverId = model.Trip.DriverId;
            trip.StartingTime = model.Trip.StartingTime;
            trip.Date = model.Trip.Date;
            trip.Capacity = model.Trip.Capacity;
            trip.Price = model.Trip.Price;*/
            // Save the changes to the database
            TempData["message"] = "<strong>Your trip has been modified successfully!</strong>";

            //ViewBag.TripMessage = "Your trip has been successfully saved!";
            return RedirectToAction("Index");

            // If the model state is not valid, return the same view with the validation errors
           // return RedirectToAction("Index", "Home");
        }
       /* public void TripCheck(EditTripViewModel model)
        {
            if (DateTime.Compare(model.Trip.Date, DateTime.Now) <= 0)
            {
                TempData["message"] = "<strong>Trip date must be in the future.</strong>";
                return RedirectToAction("Index");
            }
            RedirectToAction("Index");
        }*/
    }
}
