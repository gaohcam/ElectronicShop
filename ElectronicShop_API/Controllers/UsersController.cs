using ElectronicShop_Domain.Users;
using ElectronicShop_Model.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicShop_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly ILogger<UsersController> _logger;

		public UsersController(IUserService userService, ILogger<UsersController> logger)
		{
			_userService = userService;
			_logger = logger;
		}

		[HttpPost("Authenticate")]
		[AllowAnonymous]
		/* Dùng FromBody thì lấy từ json đã serialize bên UserApiClient truyền vô được
        còn FromForm thì lấy từ form */
		public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// Truyền request vào hàm Authencate của UserService bên Domain và trả về một JWT
			var result = await _userService.Authencate(request);

			if (!result.IsSuccessed)
			{
				return BadRequest(result);
			}

			return Ok(result);
		}

		[HttpPost("RegisterUser")]
		// Cho phép người lạ truy cập
		[AllowAnonymous]
		public async Task<IActionResult> Register([FromBody] RegisterRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _userService.Register(request);
			if (!result.IsSuccessed)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		//PUT: http://localhost/api/users/id
		[HttpPut("Update/{userId}")]
		public async Task<IActionResult> Update(Guid userId, [FromBody] UserUpdateRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _userService.Update(userId, request);
			if (!result.IsSuccessed)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		[HttpPut("RoleAssign/{userId}")]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> RoleAssign(Guid userId, [FromBody] RoleAssignRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _userService.RoleAssign(userId, request);
			if (!result.IsSuccessed)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		// Đường dẫn ví dụ của GetAllPaging
		// http://localhost/api/users/paging?pageIndex=1&pageSize=10&Keyword=
		[HttpGet("GetUsersPaging")]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> GetUsersPaging([FromQuery] GetUserPagingRequest request)
		{
			var users = await _userService.GetUsersPaging(request);
			return Ok(users);
		}

		[HttpGet("GetById/{userId}")]
		[AllowAnonymous]
		public async Task<IActionResult> GetById(Guid userId)
		{
			var user = await _userService.GetById(userId);
			return Ok(user);
		}

		[HttpGet("GetByUserName/{userName}")]
		[AllowAnonymous]
		public async Task<IActionResult> GetByUserName(string userName)
		{
			var user = await _userService.GetByUserName(userName);
			if (!user.IsSuccessed)
			{
				return BadRequest(user);
			}
			return Ok(user);
		}

		[HttpGet("GetAll")]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> GetAll()
		{
			var allUser = await _userService.GetAll();
			return Ok(allUser);
		}

		[HttpDelete("Delete/{userId}")]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Delete(Guid userId)
		{
			var result = await _userService.Delete(userId);
			return Ok(result);
		}

		[HttpPost("ChangePassword")]
		public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel model)
		{
			var result = await _userService.ChangePassword(model);
			if (!result.IsSuccessed)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		[HttpPost("ConfirmEmail")]
		[AllowAnonymous]
		public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailViewModel request)
		{
			var result = await _userService.ConfirmEmail(request);
			if (!result.IsSuccessed)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		[HttpPost("ForgotPassword")]
		[AllowAnonymous]
		public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel request)
		{
			var result = await _userService.ForgotPassword(request);
			if (!result.IsSuccessed)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		[HttpPost("ResetPassword")]
		[AllowAnonymous]
		public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel request)
		{
			var result = await _userService.ResetPassword(request);
			if (!result.IsSuccessed)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		[HttpPatch("DisableAccount/{userId}")]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> DisableAccount([FromBody] Guid userId)
		{
			var result = await _userService.DisableAccount(userId);
			if (!result.IsSuccessed)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}
	}
}