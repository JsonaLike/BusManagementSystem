using BusManagementSystem.Models;

namespace BusManagementSystem.ViewModels
{
    public class EditDriverViewModel
    {
        public Driver Driver { get; set; }
        public IEnumerable<Driver> Drivers { get; set; }
        public IEnumerable<Bus> Buses { get; set; }
        public IEnumerable<LicenseTypeMapping> LicenseTypes { get; set; }
    }
}
