using DotnetCoding.Core.Models;
using DotnetCoding.Core.Requests;

namespace DotnetCoding.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<ProductDetails>
    {
        Task<IEnumerable<ProductDetails>> GetAllActives();
        Task<IEnumerable<ProductDetails>> Search(SearchProductRequest request);
    }
}
