using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using ElectronicShop_ApiIntegration.Users;
using ElectronicShop_Model.Users;
using ElectronicShop_Utility;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace ElectronicShop_Admin.Controllers
{
	public class LoginController : Controller
	{
		private readonly IUserApiClient _userApiClient;

		// Dùng config để lấy key và issuer trong appsettings.json
		private readonly IConfiguration _configuration;

		public LoginController(
			IUserApiClient userApiClient,
			IConfiguration configuration)
		{
			_userApiClient = userApiClient;
			_configuration = configuration;
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			Response.Cookies.Delete(Constants.TOKEN);
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return Redirect(Constants.PAGE_LOGIN);
		}

		[HttpGet]
		[Route("[controller]")]
		public async Task<IActionResult> Index(string ReturnUrl = "/")
		{
			if (User.Identity.IsAuthenticated)
			{
				return LocalRedirect(ReturnUrl);
			}
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			var objLoginRequest = new LoginRequest();
			objLoginRequest.ReturnUrl = ReturnUrl;
			return View(objLoginRequest);
		}

		[HttpPost]
		[Route("[controller]")]
		public async Task<IActionResult> Index(LoginRequest request)
		{
			if (!ModelState.IsValid)
			{
				return View(request);
			}
			/* Khi đăng nhập thành công thì chúng ta sẽ giả mã token này ra có những claim gì */

			// Nhận 1 token được mã hóa
			var result = await _userApiClient.Authenticate(request);

			if (result.ResultObj == null)
			{
				// Hiển thị thông báo Tài khoản không tồn tại
				TempData["Message"] = result.Message;
				return View();
			}

			// Giải mã token đã mã hóa và lấy token, lấy cả các claim đã định nghĩa trong UserService
			// khi debug sẽ thấy nhận được gì ( có nhận được cả issuer )
			var userPrincipal = this.ValidateToken(result.ResultObj);

			if (userPrincipal.IsInRole(Constants.ROLE_NAME_ADMIN) == false)
			{
				TempData["Message"] = "Bạn không có quyền truy cập vào trang này";
				return View(request);
			}

			var authProperties = new AuthenticationProperties()
			{
				ExpiresUtc = DateTime.UtcNow.AddMonths(1),
				AllowRefresh = true,
				IsPersistent = true
			};

			var options = new CookieOptions
			{
				Expires = authProperties.ExpiresUtc,
				IsEssential = true
			};

			Response.Cookies.Append(Constants.TOKEN, result.ResultObj, options);

			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				userPrincipal,
				authProperties);

			return Redirect(request.ReturnUrl);
		}

		private ClaimsPrincipal ValidateToken(string jwtToken)
		{
			IdentityModelEventSource.ShowPII = true;

			SecurityToken validatedToken;
			var validationParameters = new TokenValidationParameters();

			validationParameters.ValidateLifetime = true;

			validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
			validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
			validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));


			// Giải mã thông tin claim mà ta đã gắn cho token ấy ( định nghĩa claim trong UserService.cs )
			var principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

			// trả về một principal có token đã giải mã
			return principal;
		}
	}
}
