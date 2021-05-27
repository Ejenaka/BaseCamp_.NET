using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoShopAPI.Models;

namespace AutoShopAPI.Repositories
{
    public class CarCollectionRepository
    {
        private static readonly List<Car> _cars;

        static CarCollectionRepository()
        {
            _cars = new List<Car>
            {
                new Car {ID = 1, Brand = "Toyota", Model="Camry", Year = 2017, Price = 23000, EngineVolume = 3.5, Mileage = 40000, Transmission = "Automatic"},
                new Car {ID = 2, Brand = "Honda", Model="Accord", Year = 2007, Price = 8000, EngineVolume = 2.4, Mileage = 250000, Transmission = "Automatic"},
                new Car {ID = 3, Brand = "Skoda", Model="Fabia", Year = 2007, Price = 5000, EngineVolume = 1.4, Mileage = 300000, Transmission = "Manual"},
                new Car {ID = 4, Brand = "Toyota", Model="Land Cruiser", Year = 2017, Price = 68900, EngineVolume = 4.6, Mileage = 33000, Transmission = "Automatic"}
            };
        }

        public void Add(Car car) => _cars.Add(car);

        public Car Get(int id)
        {
            var foundCar = _cars.Where(car => car.ID == id).SingleOrDefault();

            if (foundCar == null)
            {
                throw new ArgumentException("Car is not found");
            }

            return foundCar;
        }

        public IEnumerable<Car> GetAll() => _cars.AsEnumerable();

        public void Edit(int id, Car car)
        {
            var foundCar = Get(id);

            foundCar.Brand = car.Brand;
            foundCar.Model = car.Model;
            foundCar.Mileage = car.Mileage;
            foundCar.Price = car.Price;
            foundCar.Year = car.Year;
            foundCar.Transmission = car.Transmission;
            foundCar.EngineVolume = car.EngineVolume;
        }

        public void Delete(int id)
        {
            var foundCar = Get(id);
            _cars.Remove(foundCar);
        }
    }
}
