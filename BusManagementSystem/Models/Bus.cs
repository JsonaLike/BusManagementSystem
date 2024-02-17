using System;
using System.Collections.Generic;

namespace BusManagementSystem.Models
{
    public partial class Bus
    {
        public Bus()
        {
            Trips = new HashSet<Trip>();
        }

        public int BusId { get; set; }
        public string PlateNumber { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Capacity { get; set; }
        public int BusType { get; set; }

        public virtual BusTypeMapping BusMapping { get; set; } = null!;
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
