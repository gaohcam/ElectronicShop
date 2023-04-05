using ElectronicShop_Data.EF;
using ElectronicShop_Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace E_Commerce_Data.EF
{
	public class ElectronicShopDbContextFactory : IDesignTimeDbContextFactory<ElectronicShopDbContext>
	{
		public ElectronicShopDbContext CreateDbContext(string[] args)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var connectionString = configuration.GetConnectionString(Constants.CONNECTION_STRING);

			var optionsBuilder = new DbContextOptionsBuilder<ElectronicShopDbContext>();
			optionsBuilder.UseSqlServer(connectionString);

			return new ElectronicShopDbContext(optionsBuilder.Options);
		}
	}
}