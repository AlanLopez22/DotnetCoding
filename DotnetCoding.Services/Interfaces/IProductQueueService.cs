using DotnetCoding.Core.Responses;

namespace DotnetCoding.Services.Interfaces
{
    public interface IProductQueueService
    {
        Task<Response<IEnumerable<ProductApprovalResponse>>> GetQueueForApprovals();
        Task<Response> Approve(Guid id);
        Task<Response> Reject(Guid id);
    }
}
