using System;
using System.Collections.Generic;

namespace BusManagementSystem.Models
{
    public partial class BusTypeMapping
    {
        public int Id { get; set; }
        public string? BusType { get; set; }

        public virtual ICollection<Bus> Bus { get; set; }
    }
}
