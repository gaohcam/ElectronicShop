using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ElectronicShop_Data.Entities;
using ElectronicShop_Data.Configurations;
using ElectronicShop_Data.Extensions;

namespace ElectronicShop_Data.EF
{
	public class ElectronicShopDbContext : IdentityDbContext<AppUser, AppRole, Guid>
	{
		public ElectronicShopDbContext(DbContextOptions options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new AppConfigConfiguration());

			modelBuilder.ApplyConfiguration(new AppUserConfiguration());

			modelBuilder.ApplyConfiguration(new AppRoleConfiguration());

			modelBuilder.ApplyConfiguration(new CategoryConfiguration());

			modelBuilder.ApplyConfiguration(new ProductConfiguration());

			modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");

			// những entity có HasKey là do lúc migrate báo lỗi yêu cầu thêm key
			modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });

			modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

			modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");

			modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

			modelBuilder.Seed();
			//base.OnModelCreating(builder);
		}
		public DbSet<AppUser> AppUsers { get; set; }
		public DbSet<AppConfig> AppConfigs { get; set; }
		public DbSet<AppRole> AppRoles { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
	}
}