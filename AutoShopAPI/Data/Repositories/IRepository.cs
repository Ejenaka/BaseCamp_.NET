using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShopAPI.Repositories
{
    public interface IRepository<T>
    {
        Task<IList<T>> GetAll();
        Task<T> Get(int id);
        Task Add(T entity);
        Task Update(int id, T entity);
        Task Delete(int id);
    }
}
