using System;
using System.Collections.Generic;
using System.Text;

namespace Parser_Console.Models
{
    class House
    {
        public string Address { get; set; }

        public int NumberOfStoreys { get; set; }

        public string HouseWallType { get; set; }

        public List<Apartment> Apartments { get; set; }
    }
}
