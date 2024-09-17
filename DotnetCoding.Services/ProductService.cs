using AutoMapper;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Core.Requests;
using DotnetCoding.Core.Responses;
using DotnetCoding.Services.Constants;
using DotnetCoding.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DotnetCoding.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<ProductListResponse>>> GetAllProducts()
        {
            var products = await _unitOfWork.Products.GetAll();
            var productListResponse = _mapper.Map<IEnumerable<ProductListResponse>>(products);
            return ResponseBuilder.Create(HttpStatusCode.OK, productListResponse);
        }

        public async Task<Response<IEnumerable<ProductListResponse>>> GetActiveProducts()
        {
            var products = await _unitOfWork.Products.GetAllActives();
            var productListResponse = _mapper.Map<IEnumerable<ProductListResponse>>(products);
            return ResponseBuilder.Create(HttpStatusCode.OK, productListResponse);
        }

        public async Task<Response<IEnumerable<ProductApprovalResponse>>> GetProductApprovals()
        {
            var productQueues = await _unitOfWork.ProductQueues.GetAllForApproval();
            var productListResponse = _mapper.Map<IEnumerable<ProductApprovalResponse>>(productQueues);
            return ResponseBuilder.Create(HttpStatusCode.OK, productListResponse);
        }

        public async Task<Response<IEnumerable<ProductListResponse>>> SearchProducts(SearchProductRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);
            var products = await _unitOfWork.Products.Search(request);
            var productListResponse = _mapper.Map<IEnumerable<ProductListResponse>>(products);
            return ResponseBuilder.Create(HttpStatusCode.OK, productListResponse);
        }

        public async Task<Response> Create(CreateProductRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (request.Price > 10000)
            {
                return ResponseBuilder.Create(HttpStatusCode.BadRequest, ErrorMessages.ProductPriceOverTenThousand);
            }

            var product = CreateNewProduct(request);
            IsPriceOverFiveThousand(request.Price, product);
            _unitOfWork.Add(product);
            return await SaveAsync(ErrorMessages.CouldNotCreateProduct);
        }

        public async Task<Response> ChangePrice(ChangeProductPriceRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var product = await _unitOfWork.Products
                .Query(x => x.Id == request.ProductId)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return ResponseBuilder.Create(HttpStatusCode.NotFound, ErrorMessages.ProductNotFound);
            }

            HandleProductQueue(request, product);
            _unitOfWork.Update(product);
            return await SaveAsync(ErrorMessages.CouldNotUpdateProduct);
        }

        public async Task<Response> Delete(Guid productId)
        {
            ArgumentNullException.ThrowIfNull(productId);

            var product = await _unitOfWork.Products
                .Query(x => x.Id == productId)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return ResponseBuilder.Create(HttpStatusCode.NotFound, ErrorMessages.ProductNotFound);
            }

            return await AddProductIntoQueue(product);
        }

        private ProductDetails CreateNewProduct(CreateProductRequest request)
        {
            var product = _mapper.Map<ProductDetails>(request);
            product.Id = Guid.NewGuid();
            product.State = ProductState.Create;
            product.Status = ProductStatus.Active;
            product.PostedDate = DateTime.UtcNow;
            return product;
        }

        private static ProductQueue CreateProductQueue(QueueState state, string requestReason)
        {
            return new ProductQueue
            {
                Id = Guid.NewGuid(),
                RequestedDate = DateTime.UtcNow,
                RequestReason = requestReason,
                State = state,
            };
        }

        private static bool IsPriceOverFiveThousand(double price, ProductDetails product)
        {
            if (price > 5000)
            {
                var productQueue = CreateProductQueue(QueueState.Add, Messages.PriceMoreFiveThousand);
                product.Status = ProductStatus.ApprovalRequired;
                product.Queues.Add(productQueue);
                return true;
            }

            return false;
        }

        private static void HandleProductQueue(ChangeProductPriceRequest request, ProductDetails product)
        {
            if (request.NewPrice > (product.Price * 1.5))
            {
                var productQueue = CreateProductQueue(QueueState.Update, Messages.PriceMoreThanFiftyPercent);
                product.Status = ProductStatus.ApprovalRequired;
                product.Queues.Add(productQueue);
                return;
            }

            if (IsPriceOverFiveThousand(request.NewPrice, product))
            {
                return;
            }

            product.State = ProductState.Update;
            product.UpdatedDate = DateTime.UtcNow;
        }

        private async Task<Response> AddProductIntoQueue(ProductDetails product)
        {
            var productQueue = CreateProductQueue(QueueState.Delete, Messages.DeleteProductApproval);
            productQueue.ProductId = product.Id;
            product.Status = ProductStatus.ApprovalRequired;
            product.UpdatedDate = DateTime.UtcNow;

            _unitOfWork.Add(productQueue);
            _unitOfWork.Update(product);
            return await SaveAsync(ErrorMessages.CouldNotAddProductIntoQueue);
        }

        private async Task<Response> SaveAsync(string errorMessage)
        {
            var rowsAffected = await _unitOfWork.SaveAsync();

            if (rowsAffected == 0)
            {
                return ResponseBuilder.Create(HttpStatusCode.BadRequest, errorMessage);
            }

            return ResponseBuilder.Create(HttpStatusCode.OK);
        }
    }
}
