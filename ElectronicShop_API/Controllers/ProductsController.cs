using ElectronicShop_Model.Products;
using ElectronicShop_Domain.Product;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicShop_Admin.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;
		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetAll()
		{
			var products = await _productService.GetAll();
			return Ok(products);
		}

		[HttpGet("[action]/{productId}")]
		public async Task<IActionResult> GetProductByProductId(string productId)
		{
			var data = await _productService.GetProductByProductId(productId);
			return Ok(data);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetProductsPaging([FromQuery] GetProductsPagingRequest request)
		{
			var products = await _productService.GetProductsPaging(request);
			return Ok(products);
		}

		[HttpPost("[action]")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> CreateProduct([FromForm] ProductInput request)
		{
			var productId = await _productService.CreateProduct(request);

			var product = await _productService.GetProductByProductId(productId);

			return CreatedAtAction(nameof(GetProductByProductId), new { productId = productId! }, product);
		}

		[HttpPut("[action]/{productId}")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> UpdateProduct(
			[FromRoute] string productId,
			[FromForm] ProductInput request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			request.ProductId = productId;

			var affectedResult = await _productService.UpdateProduct(request);
			if (affectedResult == 0)
			{
				return BadRequest();
			}
			return Ok();
		}

		[HttpDelete("[action]/{productId}")]
		public async Task<IActionResult> DeleteProduct(string productId)
		{
			var affectedResult = await _productService.DeleteProduct(productId);
			if (affectedResult == 0)
			{
				return BadRequest();
			}
			return Ok();
		}
	}
}
