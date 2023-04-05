using ElectronicShop_Model.Categories;
using ElectronicShop_Model.Common;

namespace ElectronicShop_ApiIntegration.Categories
{
	public interface ICategoryApiClient
	{
		Task<List<CategoryInput>> GetAll();
		Task<ApiResult<PagedResult<CategoryInput>>> GetCategoriesPaging(GetCategoriesPagingRequest request);
		Task<CategoryInput> GetCategoryByCategoryId(Guid categoryId);
		Task<bool> CreateCategory(CategoryInput request);
		Task<bool> UpdateCategory(CategoryInput request);
		Task<bool> DeleteCategory(string categoryId);
	}
}
