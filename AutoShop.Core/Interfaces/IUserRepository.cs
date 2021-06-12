using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoShop.Core.Models;

namespace AutoShop.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task CreateCarForUser(User user, Car car);
        Task<IList<Car>> GetCarsByUser(User user);
    }
}
