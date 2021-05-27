using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AutoShopAPI.Models;
using System.Text;

namespace AutoShopAPI.Repositories
{
    public class CarFileRepository : IRepository<Car>
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();
        private readonly string _filePath = @"C:\Users\User\source\repos\BaseCamp_.NET\AutoShopAPI\Cars.json";
        private readonly List<Car> _cars;

        public CarFileRepository()
        {
            string json = File.ReadAllText(_filePath);

            _cars = JsonConvert.DeserializeObject<List<Car>>(json);
        }

        public Car Get(int id)
        {
            var foundCar = _cars.Where(c => c.ID == id).FirstOrDefault();

            return foundCar;
        }

        public IList<Car> GetAll()
        {
            return _cars;
        }

        public void Add(Car entity)
        {
            entity.ID = GetMaxID() + 1;
            _cars.Add(entity);

            UpdateRepository();
        }

        public void Delete(int id) 
        {
            var foundCar = _cars.Where(c => c.ID == id).FirstOrDefault();

            if (foundCar == null)
            {
                return;
            }

            _cars.Remove(foundCar);

            UpdateRepository();
        }

        public void Update(int id, Car entity)
        {
            var foundCar = _cars.Where(c => c.ID == id).FirstOrDefault();

            if (foundCar == null)
            {
                return;
            }

            foundCar.Brand = entity.Brand;
            foundCar.Model = entity.Model;
            foundCar.Mileage = entity.Mileage;
            foundCar.Price = entity.Price;
            foundCar.Year = entity.Year;
            foundCar.Transmission = entity.Transmission;
            foundCar.EngineVolume = entity.EngineVolume;

            UpdateRepository();
        }

        private void UpdateRepository()
        {
            string json = System.Text.Json.JsonSerializer.Serialize(_cars);
            File.WriteAllText(_filePath, json);
        }

        private int GetMaxID()
        {
            int maxID = 0;
            try
            {
                maxID = _cars.Select(c => c.ID).Max();
            }
            catch (ArgumentNullException)
            {
                return 0;
            }

            return maxID;
        }
    }
}
