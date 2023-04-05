using ElectronicShop_Model.Common;

namespace ElectronicShop_Model.Products
{
	public class GetProductsPagingRequest : PagingRequestBase
	{
		public string Keyword { get; set; }
	}
}
