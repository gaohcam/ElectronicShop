using Microsoft.AspNetCore.Identity;

namespace ElectronicShop_Data.Entities
{
	public class AppRole : IdentityRole<Guid>
	{
		public string Description { get; set; }
	}
}