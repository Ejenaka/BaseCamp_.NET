using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Core.Interfaces
{
    public interface IRepositoryManager
    {
        IUserRepository Users { get; }
        ICarRepository Cars { get; }
        Task SaveChanges();
    }
}
