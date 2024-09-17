using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Core.Requests;
using DotnetCoding.Services.Interfaces;

namespace DotnetCoding.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductDetails>> GetAllProducts()
        {
            var productDetailsList = await _unitOfWork.Products.GetAll();
            return productDetailsList;
        }

        public async Task<IEnumerable<ProductDetails>> GetActiveProducts()
        {
            return await _unitOfWork.Products.GetAllActives();
        }

        public async Task<IEnumerable<ProductDetails>> SearchProducts(SearchProductRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);
            return await _unitOfWork.Products.Search(request);
        }
    }
}
