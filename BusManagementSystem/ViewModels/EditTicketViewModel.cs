using BusManagementSystem.Models;

namespace BusManagementSystem.ViewModels
{
    public class EditTicketViewModel
    {
        public Ticket Ticket { get; set; }
        public IEnumerable<Trip> Trips { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<City> Drivers { get; set; }
        public IEnumerable<Bus> Buses { get; set; }
        public IEnumerable<Passenger> Passengers { get; set; }
    }
}
