using BusManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

namespace BusManagementSystem.Controllers
{
    //TODO:create unit tests for this project
    //TODO:add try-catch for all fuctions
    public class HomeController : Controller
    {
        private readonly ReservationSystemContext _reservationSystemContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ReservationSystemContext reservationSystemContext, ILogger<HomeController> logger)
        {
            _reservationSystemContext = reservationSystemContext;//dependency inejection -IOC(Inversion of Control)
            _logger = logger;
        }
        public IActionResult GetTrips()
        {
            var DepartureCities = _reservationSystemContext.Trips
                .Include(t => t.DepartureCityNavigation).Select(t => t.DepartureCityNavigation.Name).Distinct();
            var ArrivalCities = _reservationSystemContext.Trips
                .Include(t => t.ArrivalCityNavigation).Select(t => t.ArrivalCityNavigation.Name).Distinct();
            dynamic Cities = new JObject();
            Cities.DepartureCities = new JArray(DepartureCities);
            Cities.ArrivalCities = new JArray(ArrivalCities);
            return Ok(Cities);
        }

      

        public IActionResult Index()
        {

           // ReservationSystemContext rc= new ReservationSystemContext(); without dependency injection
            var trips = _reservationSystemContext.Trips
                .Include(t => t.DepartureCityNavigation)
                .Include(t => t.ArrivalCityNavigation).ToList();
            return View(trips);
        }
    }
}
