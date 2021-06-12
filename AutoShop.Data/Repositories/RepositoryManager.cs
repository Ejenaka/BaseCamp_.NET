using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoShop.Core.Interfaces;

namespace AutoShop.Data.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AutoShopContext _context;
        private ICarRepository _carRepository;
        private IUserRepository _userRepository;

        public RepositoryManager(AutoShopContext context)
        {
            _context = context;
        }

        public ICarRepository Cars 
        {
            get
            {
                if (_carRepository == null)
                {
                    _carRepository = new CarRepository(_context);
                }

                return _carRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }

                return _userRepository;
            }
        }
        
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
