using BusManagementSystem.Models;

namespace BusManagementSystem.ViewModels
{
    public class TripViewModel
    {
        public IEnumerable<Trip> Trips { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Driver> Drivers { get; set; }
        public IEnumerable<Bus> Buses { get; set; }
    }
}
