using System.Threading.Tasks;

namespace AutoShop.Core.Interfaces.Repositories
{
    public interface IRepositoryManager
    {
        IUserRepository Users { get; }
        ICarRepository Cars { get; }
        Task SaveChanges();
    }
}
