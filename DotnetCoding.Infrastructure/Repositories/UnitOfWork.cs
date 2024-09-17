using DotnetCoding.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextClass _dbContext;
        public IProductRepository Products { get; }
        public IProductQueueRepository ProductQueues { get; }

        public UnitOfWork(DbContextClass dbContext, IProductRepository productRepository, IProductQueueRepository productQueues)
        {
            _dbContext = dbContext;
            Products = productRepository;
            ProductQueues = productQueues;
        }

        public T Add<T>(T entity)
            where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var result = _dbContext.Set<T>().Add(entity);

            return result.Entity;
        }

        public T Update<T>(T entity)
            where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var result = _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

            return result.Entity;
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

    }
}
