using System;
using System.Collections.Generic;
using System.Text;

namespace Parser_Console.Models
{
    /// <summary>
    /// Квартира
    /// </summary>
    public class Apartment
    {
        /// <summary>
        /// Номер квартиры
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Тип квартиры (количество комнат)
        /// </summary>
        public int ApartmentType { get; set; }
        /// <summary>
        /// Тип отделки квартиры
        /// </summary>
        public string ApartmentFinishingType { get; set; }
        /// <summary>
        /// Площадь квартиры
        /// </summary>
        public float ApartmentArea { get; set; }
        /// <summary>
        /// Этажи, на которых есть данная квартиры
        /// </summary>
        public List<int> ApartmentFloorsList { get; set; }
        /// <summary>
        /// Стоимость за метр квадратный
        /// </summary>
        public int CostPerMeter { get; set; }
        /// <summary>
        /// Стоимость за квартиру
        /// </summary>
        public int CostPerApartment { get; set; }
    }
}
