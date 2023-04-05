using ElectronicShop_Model.Common;
using ElectronicShop_Model.Products;

namespace ElectronicShop_ApiIntegration.Products
{
	public interface IProductApiClient
	{
		Task<List<ProductOutput>> GetAll();
		Task<ApiResult<PagedResult<ProductOutput>>> GetProductsPaging(GetProductsPagingRequest request);
		Task<ProductOutput> GetProductByProductId(string productId);
		Task<bool> CreateProduct(ProductInput request);
		Task<bool> UpdateProduct(ProductInput request);
		Task<bool> DeleteProduct(string productId);
	}
}
