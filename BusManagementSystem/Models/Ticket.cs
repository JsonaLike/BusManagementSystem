using System;
using System.Collections.Generic;

namespace BusManagementSystem.Models
{
    public partial class Ticket 
    {
        public int PassengerId { get; set; }
        public int TripId { get; set; }

        public virtual Passenger Passenger { get; set; } = null!;
        public virtual Trip Trip { get; set; } = null!;
    }
}
