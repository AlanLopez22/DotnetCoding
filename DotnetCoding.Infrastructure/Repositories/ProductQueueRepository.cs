using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class ProductQueueRepository : GenericRepository<ProductQueue>, IProductQueueRepository
    {
        public ProductQueueRepository(DbContextClass dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<ProductQueue>> GetAllForApproval()
        {
            return await Query(x => x.State != QueueState.Rejected, i => i.Product!)
                .OrderBy(x => x.RequestedDate)
                .ToListAsync();
        }
    }
}
