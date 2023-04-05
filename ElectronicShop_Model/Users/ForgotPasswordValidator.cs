using FluentValidation;

namespace ElectronicShop_Model.Users
{
	public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordViewModel>
	{
		public ForgotPasswordValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty()
				.WithMessage("Địa chỉ Email không được để trống");
		}
	}
}