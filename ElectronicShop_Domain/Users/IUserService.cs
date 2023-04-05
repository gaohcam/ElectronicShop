using ElectronicShop_Model.Common;
using ElectronicShop_Model.Users;

namespace ElectronicShop_Domain.Users
{
	public interface IUserService
	{
		Task<ApiResult<string>> Authencate(LoginRequest request);

		Task<ApiResult<string>> Register(RegisterRequest request);

		Task<ApiResult<bool>> Update(Guid userId, UserUpdateRequest request);

		Task<List<UserViewModel>> GetAll();

		Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request);

		Task<ApiResult<UserViewModel>> GetById(Guid id);

		Task<ApiResult<UserViewModel>> GetByUserName(string userName);

		Task<ApiResult<bool>> Delete(Guid userId);

		Task<ApiResult<bool>> RoleAssign(Guid userId, RoleAssignRequest request);

		Task<ApiResult<bool>> ChangePassword(ChangePasswordViewModel request);

		Task<ApiResult<bool>> ConfirmEmail(ConfirmEmailViewModel request);

		Task<ApiResult<string>> ForgotPassword(ForgotPasswordViewModel request);

		Task<ApiResult<bool>> ResetPassword(ResetPasswordViewModel request);

		Task<ApiResult<bool>> DisableAccount(Guid userId);
	}
}
