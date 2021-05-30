using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoShopAPI.Models;
using AutoShopAPI.Data;

namespace AutoShopAPI.Repositories
{
    public class CarRepository : IRepository<Car>
    {
        private readonly AutoShopContext _context;

        public CarRepository(AutoShopContext context)
        {
            _context = context;
        }

        public async Task Add(Car entity)
        {
            await _context.Cars.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id) 
        {
            var foundCar = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(foundCar);

            await _context.SaveChangesAsync();
        }

        public async Task<Car> Get(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task<IList<Car>> GetAll()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task Update(int id, Car entity) 
        {
            var foundCar = await _context.Cars.FindAsync(id);

            if (foundCar == null)
            {
                throw new ArgumentException("Car is not found");
            }

            foundCar.Brand = entity.Brand;
            foundCar.Model = entity.Model;
            foundCar.Mileage = entity.Mileage;
            foundCar.Price = entity.Price;
            foundCar.Year = entity.Year;
            foundCar.Transmission = entity.Transmission;
            foundCar.EngineVolume = entity.EngineVolume;

            await _context.SaveChangesAsync();
        }
    }
}
