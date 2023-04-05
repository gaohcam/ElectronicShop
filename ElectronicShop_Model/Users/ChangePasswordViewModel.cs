using System.ComponentModel.DataAnnotations;

namespace ElectronicShop_Model.Users
{
	public class ChangePasswordViewModel
	{
		public string UserId { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string CurrentPassword { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}