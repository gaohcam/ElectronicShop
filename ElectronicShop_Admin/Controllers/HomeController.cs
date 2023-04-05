using ElectronicShop_Admin.Models;
using ElectronicShop_ApiIntegration.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using ElectronicShop_Utility;

namespace ElectronicShop_Admin.Controllers
{
	[Authorize]
	public class HomeController : BaseController
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUserApiClient _userApiClient;

		public HomeController(
			ILogger<HomeController> logger,
			IUserApiClient userApiClient)
		{
			_logger = logger;
			_userApiClient = userApiClient;
		}

		public async Task<IActionResult> Index()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var user = await _userApiClient.GetById(new Guid(userId));
			if (user.ResultObj.Role.Equals(Constants.ROLE_NAME_ADMIN))
			{
				return View();
			}
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return Redirect(Constants.PAGE_LOGIN);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}