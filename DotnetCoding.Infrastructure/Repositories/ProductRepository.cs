using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Core.Requests;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<ProductDetails>, IProductRepository
    {
        public ProductRepository(DbContextClass dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<ProductDetails>> GetAllActives()
        {
            return await Query(x => x.Status == ProductStatus.Active)
                .OrderByDescending(x => x.PostedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductDetails>> Search(SearchProductRequest request)
        {
            var query = Query(x => x.Status == ProductStatus.Active);

            if (!string.IsNullOrWhiteSpace(request.ProductName))
            {
                query = query.WithTerm(w => w.Name, request.ProductName);
            }

            if (request.FromPostedDate.HasValue && request.ToPostedDate.HasValue)
            {
                query = query.Where(w => w.PostedDate >= request.FromPostedDate.Value
                    && w.PostedDate <= request.ToPostedDate.Value);
            }

            if (request.MinPrice.HasValue && request.MaxPrice.HasValue)
            {
                query = query.Where(w => w.Price >= request.MinPrice.Value
                    && w.Price <= request.MaxPrice.Value);
            }

            return await query
                .OrderByDescending(x => x.PostedDate)
                .ToListAsync();
        }
    }
}
