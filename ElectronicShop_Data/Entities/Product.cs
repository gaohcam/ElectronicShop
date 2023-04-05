namespace ElectronicShop_Data.Entities
{
	public class Product
	{
		public string ProductId { get; set; }
		public string ProductName { get; set; }
		public string ProductIntroduction { get; set; }
		public string ProductDescription { get; set; }
		public Guid CategoryId { get; set; }
		public int Stock { get; set; }
		public decimal ProductPrice { get; set; }
		public decimal ProductSalePrice { get; set; }
		public string Origin { get; set; }
		public string ProductImage { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateChanged { get; set; }
		public string UserChanged { get; set; }
		public Category Category { get; set; }
	}
}
