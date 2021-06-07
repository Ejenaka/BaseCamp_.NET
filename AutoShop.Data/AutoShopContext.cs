using AutoShop.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoShop.Data
{
    public class AutoShopContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }

        public AutoShopContext(DbContextOptions<AutoShopContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
