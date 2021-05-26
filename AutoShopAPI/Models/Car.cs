using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShopAPI.Models
{
    public class Car
    {
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Price { get; set; }
        public double EngineVolume { get; set; }
        public int Mileage { get; set; }
        public string Transmission { get; set; }
    }
}
