using ElectronicShop_Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicShop_Data.Configurations
{
	internal class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.ToTable("Products");
			builder.HasKey(x => x.ProductId).IsClustered();
			builder.Property(x => x.ProductId).IsRequired();
			builder.Property(x => x.ProductName).IsRequired();
			builder.Property(x => x.ProductIntroduction).IsRequired();
			builder.Property(x => x.ProductDescription).IsRequired();
			builder.Property(x => x.Stock).IsRequired().HasDefaultValue(0);
			builder.Property(x => x.ProductPrice).IsRequired().HasDefaultValue(0);
			builder.Property(x => x.ProductSalePrice).IsRequired().HasDefaultValue(0);
			builder.Property(x => x.Origin).IsRequired();
			builder.Property(x => x.ProductImage).IsRequired();
			builder.Property(x => x.DateChanged).IsRequired();
			builder.Property(x => x.DateCreated).IsRequired();
			builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);
		}
	}
}
