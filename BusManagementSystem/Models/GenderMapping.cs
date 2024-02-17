using System;
using System.Collections.Generic;

namespace BusManagementSystem.Models
{
    public partial class GenderMapping
    {
        public int GenderId { get; set; }
        public string Gender { get; set; } = null!;
    }
}
