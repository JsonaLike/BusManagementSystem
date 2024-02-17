using BusManagementSystem.Models;
using BusManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusManagementSystem.Controllers
{
    public class SearchController : Controller
    {
        private readonly ReservationSystemContext _ReservationSystemContext;

        public SearchController(ReservationSystemContext reservationSystemContext)
        {
            _ReservationSystemContext = reservationSystemContext;
        }

        [HttpGet("/Search")]
        public IActionResult Index([FromQuery] string departureCity, [FromQuery] string arrivalCity)
        {
            var tripViewModel = new TripViewModel();
            tripViewModel.Trips = _ReservationSystemContext.Trips
     .Include(t => t.Bus).ThenInclude(b => b.BusMapping)
     .Include(t => t.DepartureCityNavigation)
     .Include(t => t.ArrivalCityNavigation)
     .Include(t => t.Driver)
     .Where(t => ( t.DepartureCityNavigation.Name==departureCity)
              && ( t.ArrivalCityNavigation.Name==arrivalCity));
            tripViewModel.Cities = _ReservationSystemContext.Cities;
            tripViewModel.Drivers = _ReservationSystemContext.Drivers;
            tripViewModel.Buses = _ReservationSystemContext.buses;

            return View(tripViewModel);
        }
    }
}
