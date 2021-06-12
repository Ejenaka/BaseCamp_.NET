using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using AutoShop.Core;
using AutoShop.Data;
using Microsoft.EntityFrameworkCore;
using AutoShop.Core.Interfaces;
using AutoShop.Core.Models;

namespace AutoShop.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AutoShopContext context)
            : base(context)
        {
        }

        public async Task CreateCarForUser(User user, Car car)
        {
            await _context.Cars.AddAsync(car);
            user.Cars.Add(car);
            
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Car>> GetCarsByUser(User user)
        {
            return await _context.Cars
                .Include(c => c.User)
                .Where(c => c.UserID == user.ID)
                .ToListAsync();
        }
    }
}
