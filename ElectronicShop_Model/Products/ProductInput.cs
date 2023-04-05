using ElectronicShop_Model.Categories;
using ElectronicShop_Utility;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ElectronicShop_Model.Products
{
	public class ProductInput
	{
		[Display(Name = Constants.FIELD_NAME_PRODUCT_PRODUCT_ID)]
		public string ProductId { get; set; }
		[Display(Name = Constants.FIELD_NAME_PRODUCT_PRODUCT_NAME)]
		public string ProductName { get; set; }
		[Display(Name = Constants.FIELD_NAME_PRODUCT_PRODUCT_INTRODUCTION)]
		public string ProductIntroduction { get; set; }
		[Display(Name = Constants.FIELD_NAME_PRODUCT_PRODUCT_DESCRIPTION)]
		public string ProductDescription { get; set; }
		[Display(Name = Constants.FIELD_NAME_PRODUCT_CATEGORY_ID)]
		public Guid CategoryId { get; set; }
		[Display(Name = Constants.FIELD_NAME_PRODUCT_STOCK)]
		public int Stock { get; set; }
		[Display(Name = Constants.FIELD_NAME_PRODUCT_PRODUCT_PRICE)]
		public decimal ProductPrice { get; set; }
		[Display(Name = Constants.FIELD_NAME_PRODUCT_PRODUCT_SALE_PRICE)]
		public decimal ProductSalePrice { get; set; }
		[Display(Name = Constants.FIELD_NAME_PRODUCT_ORIGIN)]
		public string Origin { get; set; }
		[Display(Name = Constants.FIELD_NAME_PRODUCT_PRODUCT_IMAGE)]
		public IFormFile ProductImage { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateChanged { get; set; }
		public string UserChanged { get; set; }
		public string UrlImage { get; set; }
		public List<CategoryInput> Categories { get; set; } = new List<CategoryInput>();
	}
}
