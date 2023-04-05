﻿using ElectronicShop_Data.EF;
using ElectronicShop_Data.Entities;
using ElectronicShop_Model.Common;
using ElectronicShop_Model.Users;
using ElectronicShop_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ElectronicShop_Domain.Users
{
	public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly RoleManager<AppRole> _roleManager;
		private readonly ElectronicShopDbContext _context;
		private readonly IConfiguration _config;

		public UserService(UserManager<AppUser> userManager,
			SignInManager<AppUser> signInManager,
			RoleManager<AppRole> roleManager,
            ElectronicShopDbContext context,
			IConfiguration config)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_context = context;
			_config = config;
		}

		public async Task<ApiResult<string>> Authencate(LoginRequest request)
		{
			// Tìm xem tên user có tồn tại hay không
			var user = await _userManager.FindByNameAsync(request.UserName);

			if (user == null)
				return new ApiErrorResult<string>(
					new string(Constants.ERR_USER_NAME_NOT_EXIST));

			// Trả về một SignInResult, tham số cuối là IsPersistent kiểu bool
			var result = await _signInManager.PasswordSignInAsync(
				user,
				request.Password,
				request.RememberMe, false);

			if (user.EmailConfirmed == false)
			{
				return new ApiErrorResult<string>(
					new string(Constants.ERR_USER_NOT_AUTH));
			}
			else if (result.Succeeded && user.LockoutEnabled)
			{
				return new ApiErrorResult<string>(
					new string(Constants.ERR_USER_LOCKED_ENABLE));
			}
			else if (result.Succeeded == false)
			{
				return new ApiErrorResult<string>(
					new string(Constants.ERR_USER_WRONG_PASSWORD));
			}

			var roles = await _userManager.GetRolesAsync(user);
			var claims = new[]
				{
					new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
					new Claim(ClaimTypes.Email, user.Email),
					new Claim(ClaimTypes.GivenName, user.Name),
					new Claim(ClaimTypes.Role, string.Join(";", roles)),
					new Claim(ClaimTypes.Name, request.UserName),
					new Claim(ClaimTypes.StreetAddress, user.Address),
					new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
				};
			// Lưu ý khi claim mà các thông tin bị null sẽ báo lỗi

			// Sau khi có được claim thì ta cần mã hóa nó
			// Tokens key và issuer nằm ở appsettings.json và truy cập được thông qua DI 1 Iconfig
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			// 1 SecurityToken ( cần cài jwt )
			var token = new JwtSecurityToken(_config["Tokens:Issuer"],
				_config["Tokens:Issuer"],
				claims,
				expires: DateTime.Now.AddMonths(1),
				signingCredentials: creds);

			return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
		}

		public async Task<ApiResult<string>> Register(RegisterRequest request)
		{
			var user = await _userManager.FindByNameAsync(request.UserName);
			// Kiểm tra tài khoản đã tồn tại chưa
			if (user != null)
			{
				return new ApiErrorResult<string>(
					new string(Constants.ERR_USER_NAME_EXISTED));
			}

			var email = await _userManager.FindByEmailAsync(request.Email);
			// Kiểm tra email đã tồn tại chưa
			if (email != null)
			{
				return new ApiErrorResult<string>(
					new string(Constants.ERR_USER_MAIL_EXISTED));
			}

			var usersList = await _userManager.Users.ToListAsync();
			var userPhoneNumber = usersList.FirstOrDefault(x => x.PhoneNumber == request.PhoneNumber);
			if (userPhoneNumber != null)
			{
				return new ApiErrorResult<string>(
					new string(Constants.ERR_USER_PHONE_NUMBER_USED));
			}

			if (request.Password != request.ConfirmPassword)
			{
				return new ApiErrorResult<string>(
					new string(Constants.ERR_USER_PASSWORD_NOT_MATCH));
			}

			user = new AppUser()
			{
				Email = request.Email,
				Address = request.Address,
				Name = request.Name,
				UserName = request.UserName,
				PhoneNumber = request.PhoneNumber,
			};

			var result = await _userManager.CreateAsync(user, request.Password);
			if (result.Succeeded)
			{
				user.LockoutEnabled = false;
				await _context.SaveChangesAsync();
				var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				return new ApiSuccessResult<string>(token);
			}

			return new ApiErrorResult<string>(
				new string(Constants.ERR_USER_REGIST_FAILED));
		}

		public async Task<ApiResult<bool>> Delete(Guid userId)
		{
			var user = await _userManager.FindByIdAsync(userId.ToString());
			if (user == null)
			{
				return new ApiErrorResult<bool>(
					new string(Constants.ERR_USER_NAME_NOT_EXIST));
			}
			var result = await _userManager.DeleteAsync(user);
			if (result.Succeeded)
				return new ApiSuccessResult<bool>();
			else
				return new ApiErrorResult<bool>(
					new string(Constants.ERR_USER_DELETE_FAILED));
		}

		public async Task<List<UserViewModel>> GetAll()
		{
			var query = from c in _userManager.Users
						select new { c };

			return await query.Select(x => new UserViewModel()
			{
				UserId = x.c.Id,
				Name = x.c.Name,
				UserName = x.c.UserName,
				PhoneNumber = x.c.PhoneNumber,
				Email = x.c.Email,
				Address = x.c.Address
			}).ToListAsync();
		}

		public async Task<ApiResult<UserViewModel>> GetById(Guid userId)
		{
			var user = await _userManager.FindByIdAsync(userId.ToString());
			if (user == null)
			{
				return new ApiErrorResult<UserViewModel>(
					new string(Constants.ERR_USER_NAME_NOT_EXIST));
			}
			var roles = await _userManager.GetRolesAsync(user);

			var userVm = new UserViewModel()
			{
				UserName = user.UserName,
				Address = user.Address,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				Name = user.Name,
				UserId = user.Id,
			};

			foreach (var role in roles)
			{
				userVm.Role = role.ToString();
			}

			return new ApiSuccessResult<UserViewModel>(userVm);
		}

		[AllowAnonymous]
		public async Task<ApiResult<UserViewModel>> GetByUserName(string userName)
		{
			var user = await _userManager.FindByNameAsync(userName);

			if (user == null)
			{
				return new ApiErrorResult<UserViewModel>(
					new string(Constants.ERR_USER_NAME_NOT_EXIST));
			}

			var roles = await _userManager.GetRolesAsync(user);

			var userVm = new UserViewModel()
			{
				UserName = user.UserName,
				Address = user.Address,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				Name = user.Name,
				UserId = user.Id,
			};

			foreach (var role in roles)
			{
				userVm.Role = role.ToString();
			}

			return new ApiSuccessResult<UserViewModel>(userVm);
		}

		public async Task<ApiResult<bool>> ConfirmEmail(ConfirmEmailViewModel request)
		{
			var user = await _userManager.FindByEmailAsync(request.Email);
			if (user == null)
				return new ApiErrorResult<bool>(
					new string(Constants.ERR_USER_MAIL_NOT_EXIST));

			var result = await _userManager.ConfirmEmailAsync(user, request.Token);
			return new ApiSuccessResult<bool>();
		}

		public async Task<ApiResult<string>> ForgotPassword(ForgotPasswordViewModel request)
		{
			var user = await _userManager.FindByEmailAsync(request.Email);
			if (user != null && await _userManager.IsEmailConfirmedAsync(user))
			{
				var token = await _userManager.GeneratePasswordResetTokenAsync(user);
				return new ApiSuccessResult<string>(token);
			}
			return new ApiErrorResult<string>(
				new string(Constants.ERR_USER_RESTORE_PASSWORD_FAILD));
		}

		public async Task<ApiResult<bool>> ResetPassword(ResetPasswordViewModel request)
		{
			var user = await _userManager.FindByEmailAsync(request.Email);
			if (user == null)
				return new ApiErrorResult<bool>(
					new string(Constants.ERR_USER_MAIL_NOT_EXIST));

			var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
			return new ApiSuccessResult<bool>();
		}

		// Phương thức tìm kiếm
		public async Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request)
		{
			var query = _userManager.Users;
			if (!string.IsNullOrEmpty(request.Keyword))
			{
				query = query.Where(x => x.UserName.Contains(request.Keyword)
				 || x.PhoneNumber.Contains(request.Keyword) || x.Email.Contains(request.Keyword));
			}

			//3. Paging
			int totalRow = await query.CountAsync();

			var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
				.Take(request.PageSize)
				.Select(x => new UserViewModel()
				{
					LockEnable = x.LockoutEnabled,
					Email = x.Email,
					PhoneNumber = x.PhoneNumber,
					UserName = x.UserName,
					Name = x.Name,
					UserId = x.Id,
				}).ToListAsync();

			//4. Select and projection
			var pagedResult = new PagedResult<UserViewModel>()
			{
				TotalRecords = totalRow,
				PageIndex = request.PageIndex,
				PageSize = request.PageSize,
				Items = data
			};
			return new ApiSuccessResult<PagedResult<UserViewModel>>(pagedResult);
		}

		public async Task<ApiResult<bool>> RoleAssign(Guid userId, RoleAssignRequest request)
		{
			var user = await _userManager.FindByIdAsync(userId.ToString());

			if (user == null)
			{
				return new ApiErrorResult<bool>(
					new string(Constants.ERR_USER_NAME_NOT_EXIST));
			}

			/* Khi gán quyền, người dùng bấm lưu lại thì kiểm tra xem role nào đã bỏ chọn
             * Sau đó lấy ra danh sách role đã bỏ chọn ( selected == false )
             * Dựa vào danh sách này sẽ tương tác với db và remove các role đã bị bỏ chọn khỏi user
             */
			var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
			foreach (var roleName in removedRoles)
			{
				if (await _userManager.IsInRoleAsync(user, roleName) == true)
				{
					await _userManager.RemoveFromRoleAsync(user, roleName);
				}
			}
			await _userManager.RemoveFromRolesAsync(user, removedRoles);

			/* Khi gán quyền, người dùng bấm lưu lại thì kiểm tra xem role nào đã được chọn
            * Sau đó lấy ra danh sách role đã được chọn ( selected == true )
            * Dựa vào danh sách này sẽ tương tác với db và add các role đã được chọn cho user
            */
			var addedRoles = request.Roles.Where(x => x.Selected == true).Select(x => x.Name).ToList();
			foreach (var roleName in addedRoles)
			{
				if (await _userManager.IsInRoleAsync(user, roleName) == false)
				{
					await _userManager.AddToRoleAsync(user, roleName);
				}
			}
			return new ApiSuccessResult<bool>();
		}

		public async Task<ApiResult<bool>> Update(Guid userId, UserUpdateRequest request)
		{
			// AnyAsync: bất cứ object nào mà thỏa mãn điều kiện thì sẽ trả về true
			if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != userId))
			{
				return new ApiErrorResult<bool>(
					new string(Constants.ERR_USER_MAIL_EXISTED));
			}

			// Phải to string id vì FindByIdAsync nhận tham số kiểu string
			var user = await _userManager.FindByIdAsync(userId.ToString());
			user.Email = request.Email;
			user.Name = request.Name;
			user.PhoneNumber = request.PhoneNumber;

			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
			{
				return new ApiSuccessResult<bool>();
			}

			return new ApiErrorResult<bool>(
				new string(Constants.ERR_USER_UPDATE_FAILED));
		}

		public async Task<ApiResult<bool>> ChangePassword(ChangePasswordViewModel model)
		{
			if (model.NewPassword != model.ConfirmPassword)
			{
				return new ApiErrorResult<bool>(
					new string(Constants.ERR_USER_PASSWORD_NOT_MATCH));
			}

			var user = await _userManager.FindByIdAsync(model.UserId);
			if (user.PasswordHash != HashPassword(model.CurrentPassword))
			{
				return new ApiErrorResult<bool>(
					new string(Constants.ERR_USER_WRONG_PASSWORD));
			}

			var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

			if (result.Succeeded)
			{
				await _signInManager.RefreshSignInAsync(user);
				return new ApiSuccessResult<bool>();
			}

			return new ApiErrorResult<bool>(Constants.ERR_USER_UPDATE_PASSWORD_FAILED);
		}

		public async Task<ApiResult<bool>> DisableAccount(Guid userId)
		{
			var user = await _userManager.FindByIdAsync(userId.ToString());
			var status = user.LockoutEnabled;

			switch (status)
			{
				case false:
					user.LockoutEnabled = true;
					break;

				case true:
					user.LockoutEnabled = false;
					break;
			}

			await _context.SaveChangesAsync();
			return new ApiSuccessResult<bool>();

		}

		private static string HashPassword(string password)
		{
			byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
			string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: password,
				salt: salt,
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: 100000,
				numBytesRequested: 256 / 8));
			return hashed;
		}
	}
}
