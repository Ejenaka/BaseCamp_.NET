using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoShopAPI.Models;

namespace AutoShopAPI.Data
{
    public class AutoShopContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        public AutoShopContext(DbContextOptions<AutoShopContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
