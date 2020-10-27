using System;
using System.Collections.Generic;
using System.Text;

namespace Parser_Console.Models
{
    /// <summary>
    /// Дом
    /// </summary>
    public class House
    {
        /// <summary>
        /// Дата, когда были загружены данные в прайс
        /// </summary>
        public DateTime DataReleaseDate { get; set; }
        /// <summary>
        /// Адрес дома
        /// </summary>
        public HomeAddress Address { get; set; }
        /// <summary>
        /// Этажность дома
        /// </summary>
        public int NumberOfStoreys { get; set; }
        /// <summary>
        /// Тип стен дома
        /// </summary>
        public string HouseWallType { get; set; }
        /// <summary>
        /// Очередь строительства
        /// </summary>
        public List<ConstructionPhase> ConstructionPhase { get; set; }
        /// <summary>
        /// Срок ввода в эксплуатацию секции
        /// </summary>
        public string CommissioningPeriod { get; set; }
    }
}
