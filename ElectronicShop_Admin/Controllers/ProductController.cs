using ElectronicShop_ApiIntegration.Categories;
using ElectronicShop_ApiIntegration.Products;
using ElectronicShop_Model.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ElectronicShop_Admin.Controllers
{
	[Route("[controller]")]
	[Authorize(Roles = "admin")]
	public class ProductController : Controller
	{
		private readonly IProductApiClient _productApiClient;
		private readonly ICategoryApiClient _categoryApiClient;

		public ProductController(
			IProductApiClient productApiClient,
			ICategoryApiClient categoryApiClient)
		{
			_productApiClient = productApiClient;
			_categoryApiClient = categoryApiClient;
		}

		[HttpGet]
		public async Task<IActionResult> Index(
			string keyword,
			int pageIndex = 1,
			int pageSize = 10)
		{
			var request = new GetProductsPagingRequest
			{
				Keyword = keyword,
				PageIndex = pageIndex,
				PageSize = pageSize
			};
			var data = await _productApiClient.GetProductsPaging(request);
			ViewBag.Keyword = keyword;
			return View(data.ResultObj);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> Create()
		{
			var categories = await _categoryApiClient.GetAll();
			var productInput = new ProductInput
			{
				Categories = categories
			};
			return View(productInput);
		}

		[HttpPost("[action]")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> Create([FromForm] ProductInput request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var claimsIdentity = User.Identity as ClaimsIdentity;
			request.UserChanged = claimsIdentity.FindFirst(ClaimTypes.Name).Value;

			var result = await _productApiClient.CreateProduct(request);
			if (result)
			{
				TempData["alert"] = "Tạo mới sản phẩm thành công";
				return RedirectToAction("Index");
			}

			ModelState.AddModelError("", "Thêm sản phẩm thất bại");
			return View(request);
		}

		[HttpGet("[action]/{productId}")]
		public async Task<IActionResult> Edit([FromRoute] string productId)
		{
			var categories = await _categoryApiClient.GetAll();
			var product = await _productApiClient.GetProductByProductId(productId);
			var productInput = new ProductInput()
			{
				ProductId = product.ProductId,
				ProductName = product.ProductName,
				CategoryId = product.CategoryId,
				Stock = product.Stock,
				ProductPrice = product.ProductPrice,
				ProductSalePrice = product.ProductSalePrice,
				ProductDescription = product.ProductDescription,
				ProductIntroduction = product.ProductIntroduction,
				Origin = product.Origin,
				UrlImage = product.ProductImage,
				Categories = categories
			};
			return View(productInput);
		}

		[HttpPost("[action]/{productId}")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> Edit([FromForm] ProductInput request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var claimsIdentity = User.Identity as ClaimsIdentity;
			request.UserChanged = claimsIdentity.FindFirst(ClaimTypes.Name).Value;

			var product = await _productApiClient.GetProductByProductId(request.ProductId);

			if (request.ProductImage == null)
			{
				request.UrlImage = product.ProductImage;
			}
			var result = await _productApiClient.UpdateProduct(request);
			if(result)
			{
				TempData["alert"] = "Cập nhật thông tin sản phẩm thành công";
				return RedirectToAction("Edit", "Product", new { productId = request.ProductId });
			}

			ModelState.AddModelError("", "Cập nhật thông tin sản phẩm không thành công");
			return View(request);
		}
	}
}
