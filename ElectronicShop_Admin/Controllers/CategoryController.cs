using ElectronicShop_ApiIntegration.Categories;
using ElectronicShop_Model.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ElectronicShop_Admin.Controllers
{
	[Route("[controller]")]
	[Authorize(Roles = "admin")]
	public class CategoryController : Controller
	{
		private readonly ICategoryApiClient _categoryApiClient;
		public CategoryController(ICategoryApiClient categoryApiClient)
		{
			_categoryApiClient = categoryApiClient;
		}

		[HttpGet]
		public async Task<IActionResult> Index(
			string keyword,
			int pageIndex = 1,
			int pageSize = 10)
		{
			var request = new GetCategoriesPagingRequest
			{
				Keyword = keyword,
				PageIndex = pageIndex,
				PageSize = pageSize
			};
			var data = await _categoryApiClient.GetCategoriesPaging(request);
			ViewBag.Keyword = keyword;
			return View(data.ResultObj);
		}

		[HttpGet("[action]")]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost("[action]")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> Create([FromForm] CategoryInput request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var claimsIdentity = User.Identity as ClaimsIdentity;
			request.UserChanged = claimsIdentity.FindFirst(ClaimTypes.Name).Value;

			var result = await _categoryApiClient.CreateCategory(request);
			if (result)
			{
				TempData["alert"] = "Tạo mới danh mục thành công";
				return RedirectToAction("Index");
			}

			ModelState.AddModelError("", "Thêm danh mục thất bại");
			return View(request);
		}

		[HttpGet("[action]/{categoryId:Guid}")]
		public async Task<IActionResult> Detail(Guid categoryId)
		{
			var categoryViewModel = await _categoryApiClient.GetCategoryByCategoryId(categoryId);
			var detail = new CategoryInput
			{
				CategoryId = categoryViewModel.CategoryId,
				CategoryName = categoryViewModel.CategoryName,
				DateChanged = categoryViewModel.DateChanged,
				DateCreated = categoryViewModel.DateCreated,
				UserChanged = categoryViewModel.UserChanged
			};
			return View(detail);
		}

		[HttpPost("[action]/{categoryId}")]
		public async Task<string> Delete(string categoryId)
		{
			var result = await _categoryApiClient.DeleteCategory(categoryId);
			if (result == true)
			{
				return "/Category";
			}
			ModelState.AddModelError("", "Xóa danh mục thất bại");
			return $"/Detail/{categoryId}";
		}

		[HttpGet("[action]/{categoryId:Guid}")]
		public async Task<IActionResult> Edit(Guid categoryId)
		{
			var categoryViewModel = await _categoryApiClient.GetCategoryByCategoryId(categoryId);
			var editVm = new CategoryInput
			{
				CategoryId = categoryViewModel.CategoryId,
				CategoryName = categoryViewModel.CategoryName,
				DateChanged = categoryViewModel.DateChanged,
				DateCreated = categoryViewModel.DateCreated,
				UserChanged = categoryViewModel.UserChanged
			};
			return View(editVm);
		}

		[HttpPost("[action]/{categoryId:Guid}")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> Edit([FromForm] CategoryInput request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var claimsIdentity = User.Identity as ClaimsIdentity;
			request.UserChanged = claimsIdentity.FindFirst(ClaimTypes.Name).Value;
			var result = await _categoryApiClient.UpdateCategory(request);
			if (result)
			{
				TempData["alert"] = "Cập nhật danh mục thành công";
				return RedirectToAction("Detail", "Category", new { categoryId = request.CategoryId });
			}

			ModelState.AddModelError("", "Cập nhật danh mục không thành công");
			return View(request);
		}
	}
}
