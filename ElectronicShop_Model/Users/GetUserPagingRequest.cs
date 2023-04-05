using ElectronicShop_Model.Common;

namespace ElectronicShop_Model.Users
{
	public class GetUserPagingRequest : PagingRequestBase
	{
		public string Keyword { get; set; }
	}
}
