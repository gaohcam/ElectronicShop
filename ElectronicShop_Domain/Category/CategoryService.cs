using ElectronicShop_Data.EF;
using ElectronicShop_Data.Entities;
using ElectronicShop_Model.Categories;
using ElectronicShop_Model.Common;
using ElectronicShop_Utility;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ElectronicShop_Domain.Category
{
	public class CategoryService : ICategoryService
	{
		private readonly ElectronicShopDbContext _context;
		public CategoryService(ElectronicShopDbContext context)
		{
			_context = context;
		}

		public async Task<ApiResult<string>> CreateCategory(CategoryInput request)
		{
			var categoryExisted = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == request.CategoryName);
			if (categoryExisted != null) return new ApiErrorResult<string>(
					new string(Constants.ERR_CATEGORY_EXISTED));

			var category = new ElectronicShop_Data.Entities.Category
			{
				CategoryId = request.CategoryId,
				CategoryName = request.CategoryName,
				DateCreated = DateTime.Now,
				DateChanged = DateTime.Now,
				UserChanged = request.UserChanged
			};
			_context.Categories.Add(category);
			await _context.SaveChangesAsync();
			return new ApiSuccessResult<string>(new string(category.CategoryId.ToString()));
		}

		public async Task<List<CategoryInput>> GetAll()
		{
			var query = from c in _context.Categories
						select new { c };
			return await query.Select(x => new CategoryInput
			{
				CategoryId = x.c.CategoryId,
				CategoryName = x.c.CategoryName
			}).ToListAsync();
		}

		public async Task<ApiResult<PagedResult<CategoryInput>>> GetCategoriesPaging(GetCategoriesPagingRequest request)
		{
			var query = from c in _context.Categories
						select new { c };
			if (string.IsNullOrEmpty(request.Keyword) == false)
			{
				query = query.Where(x => x.c.CategoryId.ToString().Contains(request.Keyword)
				|| x.c.CategoryName.Contains(request.Keyword));
			}

			// 3. Paing
			int totalRow = await query.CountAsync();

			var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
				.Take(request.PageSize)
				.Select(x => new CategoryInput
				{
					CategoryId = x.c.CategoryId,
					CategoryName = x.c.CategoryName
				}).ToListAsync();

			var pagedResult = new PagedResult<CategoryInput>
			{
				PageIndex = request.PageIndex,
				PageSize = request.PageSize,
				TotalRecords = totalRow,
				Items = data
			};

			return new ApiSuccessResult<PagedResult<CategoryInput>>(pagedResult);
		}

		public async Task<ApiResult<CategoryInput>> GetCategoryByCategoryId(Guid categoryId)
		{
			var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
			if (category == null)
			{
				return new ApiErrorResult<CategoryInput>(
					new string(string.Format(
					Constants.ERR_CATEGORY_NOT_EXIST,
					categoryId)));
			}
			var data = new CategoryInput
			{
				CategoryId = categoryId,
				CategoryName = category.CategoryName,
				DateCreated = category.DateCreated,
				DateChanged = category.DateChanged,
				UserChanged = category.UserChanged,
			};
			return new ApiSuccessResult<CategoryInput>(data);
		}

		public async Task<ApiResult<int>> UpdateCategory(CategoryInput request)
		{
			var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId);
			if (category == null)
			{
				return new ApiErrorResult<int>(
					new string(string.Format(
					Constants.ERR_CATEGORY_NOT_EXIST,
					request.CategoryId)));
			}

			category.CategoryName = request.CategoryName;
			category.DateChanged = DateTime.Now;
			category.UserChanged = request.UserChanged;

			return new ApiSuccessResult<int>(await _context.SaveChangesAsync());
		}

		public async Task<ApiResult<int>> DeleteCategory(Guid categoryId)
		{
			var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
			if (category == null)
			{
				return new ApiErrorResult<int>(
					new string(string.Format(
					Constants.ERR_CATEGORY_NOT_EXIST,
					categoryId)));
			}

			_context.Categories.Remove(category);
			return new ApiSuccessResult<int>(await _context.SaveChangesAsync());
		}
	}
}
