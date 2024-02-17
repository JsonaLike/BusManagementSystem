using System;
using System.Collections.Generic;

namespace BusManagementSystem.Models
{
    public partial class Driver
    {
        public Driver()
        {
            Trips = new HashSet<Trip>();
        }

        public int DriverId { get; set; }
        public string Name { get; set; } = null!;
        public int Phone { get; set; }
        public string Email { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public DateTime LicenseExpiry { get; set; }
        public int LicenseType { get; set; }

        public virtual LicenseTypeMapping LicenseTypeNavigation { get; set; } = null!;
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
