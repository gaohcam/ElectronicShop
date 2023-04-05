using System.ComponentModel.DataAnnotations;

namespace ElectronicShop_Model.Users
{
	public class RegisterRequest
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		public string UserName { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

	}
}