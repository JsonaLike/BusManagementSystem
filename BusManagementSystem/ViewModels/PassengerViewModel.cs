using BusManagementSystem.Models;

namespace BusManagementSystem.ViewModels
{
    public class PassengerViewModel
    {
        public IEnumerable<Passenger> Passengers { get; set; }
        public Dictionary<int,string> GenderMapping { get; set; }
    }
}
