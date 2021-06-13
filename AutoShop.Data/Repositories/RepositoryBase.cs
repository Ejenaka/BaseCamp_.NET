using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoShop.Core.Interfaces;

namespace AutoShop.Data.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly AutoShopContext _context;

        public RepositoryBase(AutoShopContext context)
        {
            _context = context;
        }

        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Add(T entity)      
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<IList<T>> GetAll() 
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task Update(T entity) 
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<T>> FindByCondition(Expression<Func<T, bool>> expression) 
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }
    }
}
