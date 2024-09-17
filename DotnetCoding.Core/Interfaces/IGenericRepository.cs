using System.Linq.Expressions;

namespace DotnetCoding.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        IQueryable<T> Query(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes);
    }
}
