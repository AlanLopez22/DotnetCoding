using DotnetCoding.Core.Models;
using DotnetCoding.Core.Requests;

namespace DotnetCoding.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDetails>> GetAllProducts();
        Task<IEnumerable<ProductDetails>> GetActiveProducts();
        Task<IEnumerable<ProductDetails>> SearchProducts(SearchProductRequest request);
    }
}
