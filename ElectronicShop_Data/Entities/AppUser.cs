using Microsoft.AspNetCore.Identity;

namespace ElectronicShop_Data.Entities
{
	// Guid là kiểu duy nhất cho toàn hệ thống
	public class AppUser : IdentityUser<Guid>
	{
		public string Name { get; set; }
		public string Address { get; set; }
	}
}