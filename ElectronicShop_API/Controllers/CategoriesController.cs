using ElectronicShop_Domain.Category;
using ElectronicShop_Model.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ElectronicShop_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "admin")]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryService _categoryService;

		public CategoriesController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet("[action]")]
		[AllowAnonymous]
		public async Task<IActionResult> GetAll()
		{
			var categories = await _categoryService.GetAll();
			return Ok(categories);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetCategoriesPaging([FromQuery] GetCategoriesPagingRequest request)
		{
			var categories = await _categoryService.GetCategoriesPaging(request);
			return Ok(categories);
		}

		[HttpPost("CreateCategory")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> Create([FromForm] CategoryInput request)
		{
			var claimsIdentity = User.Identity as ClaimsIdentity;
			request.UserChanged = claimsIdentity.FindFirst(ClaimTypes.GivenName).Value;

			var response = await _categoryService.CreateCategory(request);

			if (response.Message != null)
			{
				return BadRequest(response);
			}

			var category = await _categoryService.GetCategoryByCategoryId(new Guid(response.ResultObj));
			return CreatedAtAction(nameof(GetCategoryByCategoryId), new { categoryId = response.ResultObj! }, category);
		}

		[HttpGet("[action]/{categoryId}")]
		[AllowAnonymous]
		public async Task<IActionResult> GetCategoryByCategoryId(string categoryId)
		{
			var data = await _categoryService.GetCategoryByCategoryId(new Guid(categoryId));
			return Ok(data.ResultObj);
		}

		[HttpDelete("[action]/{categoryId}")]
		public async Task<IActionResult> DeleteCategory(string categoryId)
		{
			var affectedResult = await _categoryService.DeleteCategory(new Guid(categoryId));
			if (affectedResult.ResultObj == 0)
			{
				return BadRequest();
			}
			return Ok();
		}

		[HttpPut("[action]/{categoryId}")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> UpdateCategory(
			[FromRoute] string categoryId,
			[FromForm] CategoryInput request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			request.CategoryId = new Guid(categoryId);

			var claimsIdentity = User.Identity as ClaimsIdentity;
			request.UserChanged = claimsIdentity.FindFirst(ClaimTypes.GivenName).Value;

			var response = await _categoryService.UpdateCategory(request);
			if (response.ResultObj == 0)
			{
				return BadRequest(response);
			}
			return Ok();
		}
	}
}
