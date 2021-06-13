using System.Collections.Generic;
using System.Threading.Tasks;
using AutoShop.Core.Models;

namespace AutoShop.Core.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByLogin(string login);
        Task<User> GetUserByEmail(string email);
        Task CreateCarForUser(User user, Car car);
        Task<IList<Car>> GetCarsByUser(User user);
    }
}
