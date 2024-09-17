using AutoMapper;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Core.Responses;
using DotnetCoding.Services.Constants;
using DotnetCoding.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DotnetCoding.Services
{
    public class ProductQueueService : IProductQueueService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductQueueService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<ProductApprovalResponse>>> GetQueueForApprovals()
        {
            var productQueues = await _unitOfWork.ProductQueues.GetAllForApproval();
            var productListResponse = _mapper.Map<IEnumerable<ProductApprovalResponse>>(productQueues);
            return ResponseBuilder.Create(HttpStatusCode.OK, productListResponse);
        }

        public async Task<Response> Approve(Guid id)
        {
            var productQueueResponse = await ProductQueueExists(id);

            if (productQueueResponse.Response != null)
            {
                return productQueueResponse.Response;
            }

            var productQueue = productQueueResponse.Queue!;

            switch (productQueue.State)
            {
                case QueueState.Add:
                    HandleAddState(productQueue);
                    break;
                case QueueState.Delete:
                    HandleDeleteState(productQueue);
                    break;
                case QueueState.Update:
                    HandleUpdateState(productQueue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unsupported state.");
            }

            _unitOfWork.Update(productQueue);
            return await Save(ErrorMessages.CouldNotApproveProductQueue);
        }

        public async Task<Response> Reject(Guid id)
        {
            var productQueueResponse = await ProductQueueExists(id);

            if (productQueueResponse.Response != null)
            {
                return productQueueResponse.Response;
            }

            var productQueue = productQueueResponse.Queue!;
            productQueue.State = QueueState.Rejected;
            productQueue.RejectedDate = DateTime.UtcNow;
            _unitOfWork.Update(productQueue);
            return await Save(ErrorMessages.CouldNotRejectProductQueue);
        }

        private async Task<(ProductQueue? Queue, Response? Response)> ProductQueueExists(Guid id)
        {
            ArgumentNullException.ThrowIfNull(id);
            Response? response = null;
            var productQueue = await _unitOfWork.ProductQueues
                .Query(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (productQueue == null)
            {
                response = ResponseBuilder.Create(HttpStatusCode.NotFound, ErrorMessages.ProductQueueNotFound);
            }

            return (productQueue, response);
        }

        private static void HandleAddState(ProductQueue productQueue)
        {
            ArgumentNullException.ThrowIfNull(productQueue);
            ArgumentNullException.ThrowIfNull(productQueue.Product);
            productQueue.Product.Status = ProductStatus.Active;
            productQueue.Product.State = ProductState.Create;
            productQueue.Product.PostedDate = DateTime.UtcNow;
        }

        private static void HandleUpdateState(ProductQueue productQueue)
        {
            ArgumentNullException.ThrowIfNull(productQueue);
            ArgumentNullException.ThrowIfNull(productQueue.Product);
            productQueue.Product.Status = ProductStatus.Active;
            productQueue.Product.State = ProductState.Update;
            productQueue.Product.UpdatedDate = DateTime.UtcNow;
        }

        private static void HandleDeleteState(ProductQueue productQueue)
        {
            ArgumentNullException.ThrowIfNull(productQueue);
            ArgumentNullException.ThrowIfNull(productQueue.Product);
            productQueue.Product.Status = ProductStatus.Deleted;
            productQueue.Product.State = ProductState.Delete;
            productQueue.Product.DeletedDate = DateTime.UtcNow;
        }

        private async Task<Response> Save(string errorMessage)
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
