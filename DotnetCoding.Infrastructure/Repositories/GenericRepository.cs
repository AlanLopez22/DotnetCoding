using DotnetCoding.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DotnetCoding.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContextClass _dbContext;

        protected GenericRepository(DbContextClass context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public virtual IQueryable<T> Query(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes)
        {
            var query = (IQueryable<T>)_dbContext.Set<T>();

            if (condition != null)
            {
                query = query.Where(condition);
            }
            query.ToListAsync();
            return WithIncludes(query, includes);
        }

        private static IQueryable<T> WithIncludes(IQueryable<T> query, IEnumerable<Expression<Func<T, object>>> includes)
        {
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }
    }
}
