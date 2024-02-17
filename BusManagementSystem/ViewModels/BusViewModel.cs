using BusManagementSystem.Models;

namespace BusManagementSystem.ViewModels
{
    public class BusViewModel
    {
        public IEnumerable<Bus> Bus { get; set; }
        public BusTypeMapping BusTypeMapping { get; set; }
    }
}
