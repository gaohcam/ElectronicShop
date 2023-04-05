using FluentValidation;

namespace ElectronicShop_Model.Users
{
	public class ChangePasswordViewModelValidator : AbstractValidator<ChangePasswordViewModel>
	{
		public ChangePasswordViewModelValidator()
		{
			RuleFor(x => x.NewPassword)
				.NotEmpty()
				.WithMessage("Mật khẩu mới không được để trống")
				 .MinimumLength(8)
				 .WithMessage("Mật khẩu mới tối thiểu phải là 8 ký tự")
				.Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")
				.WithMessage("Mật khẩu mới không được bao gồm ký tự đặc biệt");
		}
	}
}