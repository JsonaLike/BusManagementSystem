using System;
using System.Collections.Generic;

namespace BusManagementSystem.Models
{
    public partial class Trip
    {
        public Trip()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int TripId { get; set; }
        public int DepartureCity { get; set; }
        public int ArrivalCity { get; set; }
        public int BusId { get; set; }
        public int DriverId { get; set; }
        public TimeSpan StartingTime { get; set; }
        public DateTime Date { get; set; }
        public int? Capacity { get; set; }
        public int? Reserved { get; set; }
        public Decimal? Price { get; set; }

        public virtual City ArrivalCityNavigation { get; set; } = null!;
        public virtual Bus Bus { get; set; } = null!;
        public virtual City DepartureCityNavigation { get; set; } = null!;
        public virtual Driver Driver { get; set; } = null!;
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
