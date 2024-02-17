using System;
using System.Collections.Generic;

namespace BusManagementSystem.Models
{
    public partial class Passenger 
    {
        public Passenger()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int PassengerId { get; set; }
        public string Name { get; set; } = null!;
        public int Phone { get; set; }
        public int Gender { get; set; }
        public decimal Balance { get; set; }
        public DateTime BirthDate { get; set; }
        public string? user_id { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
