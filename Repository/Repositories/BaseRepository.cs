using Domain;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext context;
        public BaseRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<T>> GetAllPaginatedFilteredAsync(Expression<Func<T, bool>> filterCriteria, int page = 1, int pageSize = 5)
        {
            var query = context.Set<T>().AsQueryable();

            if (filterCriteria != null)
            {
                query = query.Where(filterCriteria);
            }

            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllByPropertyAsync(Expression<Func<T, bool>> criteria)
        {
            return await context.Set<T>().Where(criteria).ToListAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            return entity;
        }

        public T Update(T entity)
        {
            context.Set<T>().Update(entity);
            return entity;
        }

        public T Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            return entity;
        }
    }
}