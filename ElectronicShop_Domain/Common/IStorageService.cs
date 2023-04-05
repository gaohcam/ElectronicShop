namespace ElectronicShop_Domain.Common
{
	public interface IStorageService
	{
		string GetFileUrl(string fileName);
		Task SaveFileAsync(Stream mediaBinaryStream, string fileName);
		Task DeleteFileAsync(string fileName);
	}
}
