using BusManagementSystem.Models;

namespace BusManagementSystem.ViewModels
{
    public class EditPassengerViewModel
    {
        public Passenger passenger { get; set; }
        public IEnumerable<GenderMapping> Gender { get; set; }
    }
}
