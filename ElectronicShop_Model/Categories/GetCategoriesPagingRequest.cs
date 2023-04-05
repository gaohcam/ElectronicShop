using ElectronicShop_Model.Common;

namespace ElectronicShop_Model.Categories
{
	public class GetCategoriesPagingRequest : PagingRequestBase
	{
		public string Keyword { get; set; }
	}
}
