namespace DotnetCoding.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IProductQueueRepository ProductQueues { get; }
        T Add<T>(T entity) where T : class;
        T Update<T>(T entity) where T : class;
        Task<int> SaveAsync();
    }
}
