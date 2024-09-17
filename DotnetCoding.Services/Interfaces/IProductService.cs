using DotnetCoding.Core.Requests;
using DotnetCoding.Core.Responses;

namespace DotnetCoding.Services.Interfaces
{
    public interface IProductService
    {
        Task<Response<IEnumerable<ProductListResponse>>> GetAllProducts();
        Task<Response<IEnumerable<ProductListResponse>>> GetActiveProducts();
        Task<Response<IEnumerable<ProductListResponse>>> SearchProducts(SearchProductRequest request);
        Task<Response> Create(CreateProductRequest request);
        Task<Response> Delete(Guid productId);
        Task<Response> ChangePrice(ChangeProductPriceRequest request);
    }
}
