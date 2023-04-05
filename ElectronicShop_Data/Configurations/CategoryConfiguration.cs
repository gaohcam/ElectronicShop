using ElectronicShop_Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicShop_Data.Configurations
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.ToTable("Categories");
			builder.HasKey(x => x.CategoryId).IsClustered();
			builder.Property(x => x.CategoryId).IsRequired();
			builder.Property(x => x.CategoryName).IsRequired().HasMaxLength(30);
			builder.Property(x => x.DateCreated).IsRequired();
			builder.Property(x => x.DateChanged).IsRequired();
			builder.Property(x => x.UserChanged).IsRequired();
		}
	}
}
