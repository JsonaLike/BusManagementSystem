using System;
using System.Collections.Generic;

namespace BusManagementSystem.Models
{
    public partial class City 
    {
        public City()
        {
            TripArrivalCityNavigations = new HashSet<Trip>();
            TripDepartureCityNavigations = new HashSet<Trip>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual IEnumerable<Trip> TripArrivalCityNavigations { get; set; }
        public virtual IEnumerable<Trip> TripDepartureCityNavigations { get; set; }
    }
}
