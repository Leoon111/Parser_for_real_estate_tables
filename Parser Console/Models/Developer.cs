using System;
using System.Collections.Generic;
using System.Text;

namespace Parser_Console.Models
{
    /// <summary>
    /// Застройщик
    /// </summary>
    public class Developer
    {
        /// <summary>
        /// Название застройщика
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Адреса домов данного застройщика
        /// </summary>
        public List<House> HousesAddressList { get; set; } 
    }
}
