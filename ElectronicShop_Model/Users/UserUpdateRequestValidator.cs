using FluentValidation;

namespace ElectronicShop_Model.Users
{
	public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
	{
		public UserUpdateRequestValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty()
				.WithMessage("Tên người dùng không được để trống")
				.MaximumLength(50)
				.WithMessage("Tên người dùng tối đa 50 ký tự");

			RuleFor(x => x.Email)
				.NotEmpty()
				.WithMessage("Địa chỉ Email không được để trống")
				.Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
				.WithMessage("Địa chỉ Email không đúng định dạng");

			RuleFor(x => x.UserName)
				.NotEmpty()
				.WithMessage("Tên tài khoản không được để trống");

			RuleFor(x => x.Address)
				.NotEmpty()
				.WithMessage("Địa chỉ không được để trống");

			RuleFor(x => x.PhoneNumber)
				.NotEmpty()
				.WithMessage("Số điện thoại không được để trống");
		}
	}
}