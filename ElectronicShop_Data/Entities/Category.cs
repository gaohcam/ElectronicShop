namespace ElectronicShop_Data.Entities
{
	public class Category
	{
		public Guid CategoryId { get; set; }
		public string CategoryName { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateChanged { get; set; }
		public string UserChanged { get; set; }
		public List<Product> Products { get; set; }
	}
}
