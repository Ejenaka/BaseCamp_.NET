using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShopAPI.Repositories
{
    public interface IRepository<T>
    {
        IList<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Update(int id, T entity);
        void Delete(int id);
    }
}
