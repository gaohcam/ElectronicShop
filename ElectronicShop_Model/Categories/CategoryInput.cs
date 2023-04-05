using ElectronicShop_Utility;
using System.ComponentModel.DataAnnotations;

namespace ElectronicShop_Model.Categories
{
	public class CategoryInput
	{
		[Display(Name = Constants.FIELD_NAME_CATEGORY_CATEGORY_ID)]
		public Guid CategoryId { get; set; }
		[Display(Name = Constants.FIELD_NAME_CATEGORY_CATEGORY_NAME)]
		public string CategoryName { get; set; }
		[Display(Name = Constants.FIELD_NAME_CATEGORY_DATE_CREATED)]
		public DateTime DateCreated { get; set; }
		[Display(Name = Constants.FIELD_NAME_CATEGORY_DATE_CHANGED)]
		public DateTime DateChanged { get; set; }
		[Display(Name = Constants.FIELD_NAME_CATEGORY_USER_CHANGED)]
		public string UserChanged { get; set; }
	}
}