using ElectronicShop_Model.Categories;
using ElectronicShop_Model.Common;

namespace ElectronicShop_Domain.Category
{
	public interface ICategoryService
	{
		Task<ApiResult<PagedResult<CategoryInput>>> GetCategoriesPaging(GetCategoriesPagingRequest request);
		Task<List<CategoryInput>> GetAll();
		Task<ApiResult<CategoryInput>> GetCategoryByCategoryId(Guid categoryId);
		Task<ApiResult<string>> CreateCategory(CategoryInput request);
		Task<ApiResult<int>> UpdateCategory(CategoryInput request);
		Task<ApiResult<int>> DeleteCategory(Guid categoryId);
	}
}
