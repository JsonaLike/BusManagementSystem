using BusManagementSystem.Models;

namespace BusManagementSystem.ViewModels
{
    public class TicketViewModel
    {
        public Ticket Ticket { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<City> Drivers { get; set; }
        public IEnumerable<Bus> Buses { get; set; }
    }
}
