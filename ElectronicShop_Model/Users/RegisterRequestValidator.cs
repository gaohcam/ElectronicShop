using FluentValidation;

namespace ElectronicShop_Model.Users
{
	public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
	{
		public RegisterRequestValidator()
		{
			// Đây là một phương thức của abstract validator
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

			RuleFor(x => x.PhoneNumber)
				.NotEmpty()
				.WithMessage("Số điện thoại không được để trống")
				.MaximumLength(11)
				.WithMessage("Số điện thoại không quá 11 số")
				.MinimumLength(11)
				.WithMessage("Số điện thoại tối thiểu 11 số");

			RuleFor(x => x.UserName)
				.NotEmpty()
				.WithMessage("Tên tài khoản không được để trống");

			RuleFor(x => x.Address)
				.NotEmpty()
				.WithMessage("Địa chỉ không được để trống")
				.MaximumLength(200)
				.WithMessage("Địa chỉ không được quá 200 ký tự");

			RuleFor(x => x.Password)
				.NotEmpty()
				.WithMessage("Mật khẩu không được để trống")
				.MinimumLength(8)
				.WithMessage("Mật khẩu tối thiểu 8 ký tự")
				.Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")
				.WithMessage("Mật khẩu không được chứa ký tự đặc biệt");

			// Khi ta viết => {} thì sẽ tự động hiểu request là của Register và context là của CustomContext
			//RuleFor(x => x).Custom((request, context) =>
			//  {
			//      if (request.Password != request.ConfirmPassword)
			//      {
			//          context.AddFailure("Mật khẩu xác nhận không khớp với mật khẩu");
			//      }
			//  });
		}
	}
}