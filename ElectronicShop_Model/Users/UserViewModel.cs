namespace ElectronicShop_Model.Users
{
	public class UserViewModel
	{
		public Guid UserId { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Sex { get; set; }
		public string DOB { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Role { get; set; }
		public bool LockEnable { get; set; }
	}
}
