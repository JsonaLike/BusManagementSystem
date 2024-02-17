using System;
using System.Collections.Generic;

namespace BusManagementSystem.Models
{
    public partial class LicenseTypeMapping 
    {
        public LicenseTypeMapping()
        {
            Drivers = new HashSet<Driver>();
        }

        public int LicenseTypeId { get; set; }
        public string LicenseTypeName { get; set; } = null!;

        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
