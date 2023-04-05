using System.ComponentModel.DataAnnotations;

namespace ElectronicShop_Model.Users
{
	public class ForgotPasswordViewModel
	{
		[EmailAddress]
		public string Email { get; set; }
	}
}