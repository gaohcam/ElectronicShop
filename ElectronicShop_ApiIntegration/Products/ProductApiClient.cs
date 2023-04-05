using ElectronicShop_Model.Common;
using ElectronicShop_Model.Products;
using ElectronicShop_Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ElectronicShop_ApiIntegration.Products
{
	public class ProductApiClient : BaseApiClient, IProductApiClient
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ProductApiClient(
			IHttpClientFactory httpClientFactory,
			IConfiguration configuration,
			IHttpContextAccessor httpContextAccessor)
			: base (httpClientFactory, httpContextAccessor, configuration)
		{
			_httpClientFactory = httpClientFactory;
			_configuration = configuration;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<bool> CreateProduct(ProductInput request)
		{
			var url = string.Format(
				Constants.API_PRODUCT,
				nameof(CreateProduct));

			var content = new MultipartFormDataContent();
			content.Add(new StringContent(request.ProductId), "productId");
			content.Add(new StringContent(request.ProductName), "productName");
			content.Add(new StringContent(request.ProductIntroduction), "productIntroduction");
			content.Add(new StringContent(request.ProductDescription), "productDescription");
			content.Add(new StringContent(request.CategoryId.ToString()), "categoryId");
			content.Add(new StringContent(request.Stock.ToString()), "stock");
			content.Add(new StringContent(request.ProductPrice.ToString()), "productPrice");
			content.Add(new StringContent(request.ProductSalePrice.ToString()), "productSalePrice");
			content.Add(new StringContent(request.Origin), "origin");
			content.Add(new StringContent(request.DateCreated.ToString()), "dateCreated");
			content.Add(new StringContent(request.DateChanged.ToString()), "dateChanged");
			content.Add(new StringContent(request.UserChanged), "userChanged");

			if (request.ProductImage != null)
			{
				byte[] data;
				using (var br = new BinaryReader(request.ProductImage.OpenReadStream()))
				{
					data = br.ReadBytes((int)request.ProductImage.OpenReadStream().Length);
				}
				ByteArrayContent bytes = new ByteArrayContent(data);
				content.Add(bytes, "productImage", request.ProductImage.FileName);
			}

			var result = await Post(url, content);
			return result;
		}

		public async Task<bool> DeleteProduct(string productId)
		{
			var url = string.Format(
				Constants.API_PRODUCT,
				nameof(DeleteProduct) + '/' + productId);

			var result = await Delete(url);
			return result;
		}

		public async Task<List<ProductOutput>> GetAll()
		{
			var url = string.Format(
				Constants.API_PRODUCT,
				nameof(GetAll));

			var products = await GetListAsync<ProductOutput>(url);
			return products;
		}

		public async Task<ProductOutput> GetProductByProductId(string productId)
		{
			var url = string.Format(
					Constants.API_PRODUCT,
					nameof(GetProductByProductId) + "/" + productId);

			var data = await GetAsync<ProductOutput>(url);
			return data;
		}

		public async Task<ApiResult<PagedResult<ProductOutput>>> GetProductsPaging(GetProductsPagingRequest request)
		{
			var url = string.Format(
					Constants.API_PRODUCT,
					nameof(GetProductsPaging));
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

			var products = await GetAsync<ApiSuccessResult<PagedResult<ProductOutput>>>(url, true);

			return products;
		}

		public async Task<bool> UpdateProduct(ProductInput request)
		{
			var url = string.Format(
					Constants.API_PRODUCT,
					nameof(UpdateProduct) + "/" + request.ProductId);

			var content = new MultipartFormDataContent();
			content.Add(new StringContent(request.ProductId), "productId");
			content.Add(new StringContent(request.ProductName), "productName");
			content.Add(new StringContent(request.ProductIntroduction), "productIntroduction");
			content.Add(new StringContent(request.ProductDescription), "productDescription");
			content.Add(new StringContent(request.CategoryId.ToString()), "categoryId");
			content.Add(new StringContent(request.Stock.ToString()), "stock");
			content.Add(new StringContent(request.ProductPrice.ToString()), "productPrice");
			content.Add(new StringContent(request.ProductSalePrice.ToString()), "productSalePrice");
			content.Add(new StringContent(request.Origin), "origin");
			content.Add(new StringContent(request.DateCreated.ToString()), "dateCreated");
			content.Add(new StringContent(request.DateChanged.ToString()), "dateChanged");
			content.Add(new StringContent(request.UserChanged), "userChanged");

			if (request.ProductImage != null)
			{
				byte[] data;
				using (var br = new BinaryReader(request.ProductImage.OpenReadStream()))
				{
					data = br.ReadBytes((int)request.ProductImage.OpenReadStream().Length);
				}
				ByteArrayContent bytes = new ByteArrayContent(data);
				content.Add(bytes, "productImage", request.ProductImage.FileName);
			}

			var result = await Put(url, content);
			return result;
		}
	}
}
