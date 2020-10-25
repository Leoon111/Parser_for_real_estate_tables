using System;
using System.Collections.Generic;
using System.Text;

namespace Parser_Console.Models
{
    public class Apartment
    {
        public int Number { get; set; }

        public string ApartmentType { get; set; }

        public string ApartmentFinishingType { get; set; }

        public float ApartmentArea { get; set; }

        public List<int> ApartmentFloors { get; set; }

        public int CostPerMeter { get; set; }

        public int CostPerApartment { get; set; }
    }
}
