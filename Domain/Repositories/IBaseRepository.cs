using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllPaginatedFilteredAsync(Expression<Func<T, bool>> filterCriteria, int page = 1, int count = 5);
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
