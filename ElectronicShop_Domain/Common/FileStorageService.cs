using ElectronicShop_Utility;
using Microsoft.AspNetCore.Hosting;

namespace ElectronicShop_Domain.Common
{
	public class FileStorageService : IStorageService
	{
		private readonly string _productContentFolder;

		public FileStorageService(IWebHostEnvironment webHostEnvironment)
		{
			_productContentFolder = Path.Combine(webHostEnvironment.WebRootPath, Constants.PRODUCT_CONTENT_FOLDER_NAME);
		}

		public string GetFileUrl(string fileName)
		{
			return $"/{Constants.PRODUCT_CONTENT_FOLDER_NAME}/{fileName}";
		}

		public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
		{
			var filePath = Path.Combine(_productContentFolder, fileName);
			using var output = new FileStream(filePath, FileMode.Create);
			await mediaBinaryStream.CopyToAsync(output);
			mediaBinaryStream.Close();
		}

		public async Task DeleteFileAsync(string fileName)
		{
			var filePath = Path.Combine(_productContentFolder, fileName);
			if (File.Exists(filePath))
			{
				await Task.Run(() => File.Delete(filePath));
			}
		}
	}
}
