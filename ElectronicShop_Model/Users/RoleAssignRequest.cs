using ElectronicShop_Model.Common;

namespace ElectronicShop_Model.Users
{
	public class RoleAssignRequest
	{
		public Guid RoleId { get; set; }
		public List<SelectItem> Roles { get; set; } = new List<SelectItem>();

	}
}