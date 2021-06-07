using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoShop.Core;
using AutoShop.Core.Models;

namespace AutoShop.API.Requests.Cars
{
    public class CarCreateRequest
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Price { get; set; }
        public double EngineVolume { get; set; }
        public int Mileage { get; set; }
        public string Transmission { get; set; }
    }
}
