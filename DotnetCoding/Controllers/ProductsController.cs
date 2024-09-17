using Microsoft.AspNetCore.Mvc;
using DotnetCoding.Services.Interfaces;
using DotnetCoding.Core.Requests;

namespace DotnetCoding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly IProductService _productService;
        public readonly IProductQueueService _productQueueService;

        public ProductsController(IProductService productService, IProductQueueService productQueueService)
        {
            _productService = productService;
            _productQueueService = productQueueService;
        }

        /// <summary>
        /// Get the list of product.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            var productDetailsList = await _productService.GetAllProducts();

            return Ok(productDetailsList);
        }

        /// <summary>
        /// Get the list of active products.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/active")]
        public async Task<IActionResult> GetActiveProductList()
        {
            var productDetailsList = await _productService.GetActiveProducts();

            return Ok(productDetailsList);
        }

        /// <summary>
        /// Search for products.
        /// </summary>
        /// <returns></returns>
        [HttpPost("search")]
        public async Task<IActionResult> SearchProducts(SearchProductRequest request)
        {
            var productDetailsList = await _productService.SearchProducts(request);

            return Ok(productDetailsList);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductRequest request)
        {
            var productDetails = await _productService.Create(request);

            return Ok(productDetails);
        }

        /// <summary>
        /// Request for a price change.
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> ChangePrice(ChangeProductPriceRequest request)
        {
            var productDetails = await _productService.ChangePrice(request);

            return Ok(productDetails);
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var productDetails = await _productService.Delete(id);

            return Ok(productDetails);
        }
    }
}
