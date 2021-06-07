using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoShop.Core;
using AutoShop.Data;
using AutoShop.Data.Repositories;
using AutoShop.Core.Interfaces;
using AutoShop.Core.Models;

namespace AutoShop.Data.Repositories
{
    public class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(AutoShopContext context)
            : base(context)
        {
        }

        //public override async Task<Car> Get(int id) 
        //{
        //    var foundCars = await FindByCondition(car => car.ID == id);

        //    return foundCars.FirstOrDefault();
        //}
    }
}
