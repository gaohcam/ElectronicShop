using ElectronicShop_Model.Common;
using ElectronicShop_Model.Products;

namespace ElectronicShop_Domain.Product
{
	public interface IProductService
	{
		Task<ProductOutput> GetProductByProductId(string productId);
		Task<ApiResult<PagedResult<ProductOutput>>> GetProductsPaging(GetProductsPagingRequest request);
		Task<List<ProductOutput>> GetAll();
		Task<string> CreateProduct(ProductInput request);
		Task<int> UpdateProduct(ProductInput request);
		Task<int> DeleteProduct(string productId);
	}
}
