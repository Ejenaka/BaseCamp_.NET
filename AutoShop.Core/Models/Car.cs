using System;

namespace AutoShop.Core.Models
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
        DateTime PostedDate { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }


        public Car()
        {
            PostedDate = DateTime.UtcNow;
        }
    }
}
