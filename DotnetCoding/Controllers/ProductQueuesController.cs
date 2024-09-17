using DotnetCoding.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoding.Controllers
{
    [Route("api/products/queues")]
    [ApiController]
    public class ProductQueuesController : ControllerBase
    {
        public readonly IProductQueueService _productQueueService;

        public ProductQueuesController(IProductQueueService productQueueService)
        {
            _productQueueService = productQueueService;
        }

        /// <summary>
        /// Get the list of queue for approval.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetQueueForApprovals()
        {
            var productDetailsList = await _productQueueService.GetQueueForApprovals();

            return Ok(productDetailsList);
        }

        /// <summary>
        /// Request for a price change.
        /// </summary>
        /// <returns></returns>
        [HttpPatch("approve/{id}")]
        public async Task<IActionResult> Approve([FromRoute] Guid id)
        {
            var productDetails = await _productQueueService.Approve(id);

            return Ok(productDetails);
        }

        /// <summary>
        /// Request for a price change.
        /// </summary>
        /// <returns></returns>
        [HttpPatch("reject/{id}")]
        public async Task<IActionResult> Reject([FromRoute] Guid id)
        {
            var productDetails = await _productQueueService.Reject(id);

            return Ok(productDetails);
        }

    }
}
