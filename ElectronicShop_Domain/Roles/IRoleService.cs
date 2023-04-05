using ElectronicShop_Model.Roles;

namespace ElectronicShop_Domain.Roles
{
	public interface IRoleService
	{
		Task<List<RoleViewModel>> GetAll();
	}
}