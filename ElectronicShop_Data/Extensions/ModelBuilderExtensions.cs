using ElectronicShop_Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ElectronicShop_Data.Extensions
{
	public static class ModelBuilderExtensions
	{
		public static void Seed(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AppConfig>().HasData(
			   new AppConfig() { Key = "HomeTitle", Value = "This is home page of Electronic Shop" },
			   new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of Electronic Shop" },
			   new AppConfig() { Key = "HomeDescription", Value = "This is description of Electronic Shop" }
			   );

			// any guid
			var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
			var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
			modelBuilder.Entity<AppRole>().HasData(new AppRole
			{
				Id = roleId,
				Name = "admin",
				NormalizedName = "admin",
				Description = "Administrator role"
			});

			var hasher = new PasswordHasher<AppUser>();
			modelBuilder.Entity<AppUser>().HasData(new AppUser
			{
				Id = adminId,
				UserName = "admin",
				NormalizedUserName = "admin",
				Email = "lequocanh.qa@gmail.com",
				NormalizedEmail = "lequocanh.qa@gmail.com",
				EmailConfirmed = true,
				PasswordHash = hasher.HashPassword(null, "Abcd1234"),
				SecurityStamp = string.Empty,
				PhoneNumber = "0774642207",
				Address = "123 Lien Ap 2-6 X.Vinh Loc A H. Binh Chanh",
				Name = "Quoc Anh",
			});

			modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
			{
				RoleId = roleId,
				UserId = adminId
			});

			modelBuilder.Entity<Category>().HasData(
				new Category() { CategoryId = new Guid("73ACA840-B158-4734-A234-90F75B6D6192"), CategoryName = "Tủ lạnh", DateCreated = DateTime.Now, DateChanged = DateTime.Now, UserChanged = "admin" },
				new Category() { CategoryId = new Guid("805FF97F-8450-4C87-B078-C851AB4C02A7"), CategoryName = "Máy lạnh", DateCreated = DateTime.Now, DateChanged = DateTime.Now, UserChanged = "admin" },
				new Category() { CategoryId = new Guid("1C74ED19-9FF1-44AE-86E2-0998344057F7"), CategoryName = "Máy giặt", DateCreated = DateTime.Now, DateChanged = DateTime.Now, UserChanged = "admin" }
			);
		}
	}
}