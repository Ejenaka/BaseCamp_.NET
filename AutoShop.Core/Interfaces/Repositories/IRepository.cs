using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AutoShop.Core.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        Task<IList<T>> GetAll();
        Task<T> Get(int id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<IList<T>> FindByCondition(Expression<Func<T, bool>> expression);
    }
}
