using System.ComponentModel.DataAnnotations;

namespace ElectronicShop_Model.Users
{
	public class ResetPasswordViewModel
	{
		[EmailAddress]
		public string Email { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[DataType(DataType.Password)]
		[Compare(
			"Password",
			ErrorMessage = "Mật khẩu xác nhận không khớp với mật khẩu")]
		public string ConfirmPassword { get; set; }
		public string Token { get; set; }
	}
}