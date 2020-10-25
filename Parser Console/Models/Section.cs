using System;
using System.Collections.Generic;
using System.Text;

namespace Parser_Console.Models
{
    public class Section
    {
        public int Number { get; set; }

        public string CommissioningPeriod { get; set; }

        public int NumberOfStoreys { get; set; }

        public string HouseWallType { get; set; }

        public List<int> EntranceNumbers { get; set; }
    }
}
