using ElectronicShop_Model.Categories;
using ElectronicShop_Model.Common;
using ElectronicShop_Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ElectronicShop_ApiIntegration.Categories
{
	public class CategoryApiClient : BaseApiClient, ICategoryApiClient
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CategoryApiClient(
			IHttpClientFactory httpClientFactory,
			IHttpContextAccessor httpContextAccessor,
			IConfiguration configuration)
			: base(httpClientFactory, httpContextAccessor, configuration)
		{
			_httpContextAccessor = httpContextAccessor;
			_configuration = configuration;
			_httpClientFactory = httpClientFactory;
		}

		public async Task<bool> CreateCategory(CategoryInput request)
		{
			var url = string.Format(
				Constants.API_CATEGORY,
				nameof(CreateCategory));

			var content = new MultipartFormDataContent();
			content.Add(new StringContent(request.CategoryName), "categoryName");
			content.Add(new StringContent(request.UserChanged), "userChanged");

			var result = await Post(url, content);
			return result;
		}

		public async Task<bool> DeleteCategory(string categoryId)
		{
			var url = string.Format(
				Constants.API_CATEGORY,
				nameof(DeleteCategory) + '/' + categoryId);

			var result = await Delete(url);
			return result;
		}

		public async Task<List<CategoryInput>> GetAll()
		{
			var url = string.Format(
				Constants.API_CATEGORY,
				nameof(GetAll));

			var categories = await GetListAsync<CategoryInput>(url);
			return categories;
		}

		public async Task<ApiResult<PagedResult<CategoryInput>>> GetCategoriesPaging(GetCategoriesPagingRequest request)
		{
			var url = string.Format(
					Constants.API_CATEGORY,
					nameof(GetCategoriesPaging));
			url = string.Concat(
				url,
				string.Join(
					"&",
					string.Concat(
						"?pageIndex=",
						request.PageIndex),
					string.Concat(
						"pageSize=",
						request.PageSize),
					string.Concat(
						"keyword=",
						request.Keyword)));

			var categories = await GetAsync<ApiSuccessResult<PagedResult<CategoryInput>>>(url, true);

			return categories;
		}

		public async Task<CategoryInput> GetCategoryByCategoryId(Guid categoryId)
		{
			var url = string.Format(
					Constants.API_CATEGORY,
					nameof(GetCategoryByCategoryId) + "/" + categoryId);

			var data = await GetAsync<CategoryInput>(url);
			return data;
		}

		public async Task<bool> UpdateCategory(CategoryInput request)
		{
			var url = string.Format(
					Constants.API_CATEGORY,
					nameof(UpdateCategory) + "/" + request.CategoryId);

			var requestContent = new MultipartFormDataContent();
			requestContent.Add(
				new StringContent(
					string.IsNullOrEmpty(request.CategoryName) ? "" : request.CategoryName.ToString()),
				"categoryName");
			requestContent.Add(
				new StringContent(request.UserChanged),
				"userChanged");

			var result = await Put(url, requestContent);
			return result;
		}
	}
}
