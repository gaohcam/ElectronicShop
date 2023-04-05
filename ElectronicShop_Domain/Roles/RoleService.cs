using ElectronicShop_Data.Entities;
using ElectronicShop_Model.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ElectronicShop_Domain.Roles
{
	public class RoleService : IRoleService
	{
		private readonly RoleManager<AppRole> _roleManager;
		public RoleService(RoleManager<AppRole> roleManager)
		{
			_roleManager = roleManager;
		}
		public async Task<List<RoleViewModel>> GetAll()
		{
			var roles = await _roleManager.Roles
				.Select(x => new RoleViewModel()
				{
					RoleId = x.Id,
					Name = x.Name,
					Description = x.Description,

				}).ToListAsync();

			return roles;
		}
	}
}