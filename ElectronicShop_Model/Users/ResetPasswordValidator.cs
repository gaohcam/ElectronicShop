using FluentValidation;

namespace ElectronicShop_Model.Users
{
	public class ResetPasswordValidator : AbstractValidator<ResetPasswordViewModel>
	{
		public ResetPasswordValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty()
				.WithMessage("Địa chỉ Email không được để trống");

			RuleFor(x => x.Password)
				.NotEmpty()
				.WithMessage("Mật khẩu không được để trống")
				.MinimumLength(8)
				.WithMessage("Mật khẩu tối thiểu 8 ký tự")
				.Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")
				.WithMessage("Mật khẩu không được chứa ký tự đặc biệt");
		}
	}
}