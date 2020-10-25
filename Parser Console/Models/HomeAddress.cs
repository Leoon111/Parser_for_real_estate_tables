using System;
using System.Collections.Generic;
using System.Text;

namespace Parser_Console.Models
{
    /// <summary>
    /// Адрес дома
    /// </summary>
    public class HomeAddress
    {
        /// <summary>
        /// Улица
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Дом номер
        /// </summary>
        public int HomeNumber { get; set; }
    }
}
