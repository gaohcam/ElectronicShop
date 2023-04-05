namespace ElectronicShop_Utility
{
	public class ElectronicShopException : Exception
	{
		public ElectronicShopException()
		{
		}

		public ElectronicShopException(string message)
			: base(message)
		{
		}

		public ElectronicShopException(string message, Exception ex)
			: base(message, ex)
		{
		}
	}
}
