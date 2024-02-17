using BusManagementSystem.Models;

namespace BusManagementSystem.ViewModels
{
    public class EditBusViewModel
    {
        public Bus Bus { get; set; }
        public IEnumerable<BusTypeMapping> BusTypeMapping { get; set; }
        public bool BusTypeChoice { get; set; }
        public string BusType { get; set; }
    }
}
