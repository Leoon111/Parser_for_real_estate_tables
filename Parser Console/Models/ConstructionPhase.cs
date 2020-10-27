using System;
using System.Collections.Generic;
using System.Text;

namespace Parser_Console.Models
{
    /// <summary>
    /// Этап строительства
    /// </summary>
    public class ConstructionPhase
    {
        /// <summary>
        /// Номер секции строительства
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Срок ввода в эксплуатацию секции
        /// </summary>
        public string CommissioningPeriod { get; set; }
        /// <summary>
        /// Этажность секции
        /// </summary>
        public int NumberOfStoreys { get; set; }
        /// <summary>
        /// Тип стен секции
        /// </summary>
        public string HouseWallType { get; set; }
        /// <summary>
        /// Номера подъездов в секции
        /// </summary>
        public List<int> NumberOfPorchesHouseList { get; set; }
        /// <summary>
        /// Квартиры в секции
        /// </summary>
        public List<Apartment> ApartmentsList { get; set; }
    }
}
