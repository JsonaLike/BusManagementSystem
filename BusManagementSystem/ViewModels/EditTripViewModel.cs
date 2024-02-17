using BusManagementSystem.Models;

namespace BusManagementSystem.ViewModels
{
    public class EditTripViewModel
    {
        public Trip Trip { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Driver> Drivers { get; set; }
        public IEnumerable<Bus> Buses { get; set; }
    }
}
