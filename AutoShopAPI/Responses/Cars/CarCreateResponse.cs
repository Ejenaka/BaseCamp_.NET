using System;
using AutoShop.Core.Models;

namespace AutoShop.API.Responses.Cars
{
    public class CarCreateResponse
    {
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Price { get; set; }
        public double EngineVolume { get; set; }
        public int Mileage { get; set; }
        public string Transmission { get; set; }
        public DateTime PostedDate { get; set; }
        public int UserID { get; set; }
    }
}
