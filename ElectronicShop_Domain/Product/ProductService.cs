using ElectronicShop_Data.EF;
using ElectronicShop_Data.Entities;
using ElectronicShop_Domain.Common;
using ElectronicShop_Model.Common;
using ElectronicShop_Model.Products;
using ElectronicShop_Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace ElectronicShop_Domain.Product
{
	public class ProductService : IProductService
	{
		private readonly IStorageService _storageService;
		private readonly IConfiguration _configuration;
		private const string PRODUCT_CONTENT_FOLDER_NAME = "product-content";
		private readonly ElectronicShopDbContext _context;
		public ProductService(
			ElectronicShopDbContext context,
			IStorageService storageService,
			IConfiguration configuration)
		{
			_context = context;
			_storageService = storageService;
			_configuration = configuration;
		}

		public async Task<string> CreateProduct(ProductInput request)
		{
			var productExisted = _context.Products.FirstOrDefault(p =>
			p.ProductName == request.ProductName
			|| p.ProductId == request.ProductId);

			if (productExisted != null)
			{
				throw new ElectronicShopException(Constants.ERR_PRODUCT_EXISTED);
			}

			var product = new ElectronicShop_Data.Entities.Product
			{
				ProductId = request.ProductId,
				ProductName = request.ProductName,
				ProductDescription = request.ProductDescription,
				ProductIntroduction = request.ProductIntroduction,
				Stock = request.Stock,
				ProductPrice = request.ProductPrice,
				ProductSalePrice = request.ProductSalePrice,
				Origin = request.Origin,
				CategoryId = request.CategoryId,
				DateCreated = DateTime.Now,
				DateChanged = DateTime.Now,
				UserChanged = request.UserChanged
			};

			//Save product image
			if (request.ProductImage != null)
			{
				product.ProductImage = await this.SaveFile(request.ProductImage);
			}
			else
			{
				product.ProductImage = "no-image.png";
			}

			_context.Products.Add(product);
			await _context.SaveChangesAsync();
			return product.ProductId;
		}

		public async Task<int> DeleteProduct(string productId)
		{
			var productExisted = _context.Products.FirstOrDefault(p => p.ProductId == productId);
			if (productExisted == null)
			{
				throw new ElectronicShopException(string.Format(
					Constants.ERR_CATEGORY_NOT_EXIST,
					productId));
			}

			_context.Products.Remove(productExisted);
			return await _context.SaveChangesAsync();
		}

		public async Task<List<ProductOutput>> GetAll()
		{
			var query = from p in _context.Products
						join c in _context.Categories on p.CategoryId equals c.CategoryId
						select new { c, p };

			return await query.Select(x => new ProductOutput
			{
				ProductId = x.p.ProductId,
				ProductName = x.p.ProductName,
				ProductDescription = x.p.ProductDescription,
				ProductIntroduction = x.p.ProductIntroduction,
				CategoryId = x.p.CategoryId,
				Stock = x.p.Stock,
				ProductPrice = x.p.ProductPrice,
				ProductSalePrice = x.p.ProductSalePrice,
				Origin = x.p.Origin,
				ProductImage = _storageService.GetFileUrl(x.p.ProductImage),
				DateCreated = x.p.DateCreated,
				DateChanged = x.p.DateChanged,
				UserChanged = x.p.UserChanged,
				CategoryName = x.c.CategoryName
			}).ToListAsync();
		}

		public async Task<ProductOutput> GetProductByProductId(string productId)
		{
			var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
			if (product == null) throw new ElectronicShopException(
				string.Format(
					Constants.ERR_PRODUCT_NOT_EXIST,
					productId));

			var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == product.CategoryId);

			var data = new ProductOutput
			{
				ProductId = product.ProductId,
				ProductName = product.ProductName,
				ProductDescription = product.ProductDescription,
				ProductIntroduction = product.ProductIntroduction,
				CategoryId = product.CategoryId,
				Stock = product.Stock,
				ProductPrice = product.ProductPrice,
				ProductSalePrice = product.ProductSalePrice,
				Origin = product.Origin,
				ProductImage = _storageService.GetFileUrl(product.ProductImage),
				DateCreated = product.DateCreated,
				DateChanged = product.DateChanged,
				UserChanged = product.UserChanged,
				CategoryName = category.CategoryName,
			};
			return data;
		}

		public async Task<ApiResult<PagedResult<ProductOutput>>> GetProductsPaging(GetProductsPagingRequest request)
		{
			var query = from p in _context.Products
						join c in _context.Categories on p.CategoryId equals c.CategoryId
						select new { c, p };
			if (string.IsNullOrEmpty(request.Keyword) == false)
			{
				query = query.Where(x => x.p.ProductId.Contains(request.Keyword)
				|| x.p.ProductName.Contains(request.Keyword));
			}

			// 3. Paing
			int totalRow = await query.CountAsync();

			var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
				.Take(request.PageSize)
				.Select(x => new ProductOutput
				{
					ProductId = x.p.ProductId,
					ProductName = x.p.ProductName,
					ProductDescription = x.p.ProductDescription,
					ProductIntroduction = x.p.ProductIntroduction,
					CategoryId = x.p.CategoryId,
					Stock = x.p.Stock,
					ProductPrice = x.p.ProductPrice,
					ProductSalePrice = x.p.ProductSalePrice,
					Origin = x.p.Origin,
					ProductImage = _storageService.GetFileUrl(x.p.ProductImage),
					DateCreated = x.p.DateCreated,
					DateChanged = x.p.DateChanged,
					UserChanged = x.p.UserChanged,
					CategoryName = x.c.CategoryName
				}).ToListAsync();

			var pagedResult = new PagedResult<ProductOutput>
			{
				PageIndex = request.PageIndex,
				PageSize = request.PageSize,
				TotalRecords = totalRow,
				Items = data
			};

			return new ApiSuccessResult<PagedResult<ProductOutput>>(pagedResult);
		}

		public async Task<int> UpdateProduct(ProductInput request)
		{
			var productExisted = _context.Products.FirstOrDefault(p => p.ProductId == request.ProductId);
			if (productExisted == null)
			{
				throw new ElectronicShopException(string.Format(
					Constants.ERR_CATEGORY_NOT_EXIST,
					request.ProductId));
			}

			productExisted.ProductName = request.ProductName;
			productExisted.ProductDescription = request.ProductDescription;
			productExisted.ProductIntroduction = request.ProductIntroduction;
			productExisted.CategoryId = request.CategoryId;
			productExisted.Stock = request.Stock;
			productExisted.ProductPrice = request.ProductPrice;
			productExisted.ProductSalePrice = request.ProductSalePrice;
			productExisted.Origin = request.Origin;
			productExisted.DateCreated = request.DateCreated;
			productExisted.DateChanged = request.DateChanged;
			productExisted.UserChanged = request.UserChanged;

			var oldImage = productExisted.ProductImage;
			//Save product image
			if (request.ProductImage != null)
			{
				productExisted.ProductImage = await this.SaveFile(request.ProductImage);
				if ((string.IsNullOrEmpty(oldImage) == false) && (oldImage.Equals(Constants.DEFAUT_IMAGE_FILE) == false))
					await _storageService.DeleteFileAsync(oldImage);
			}

			return await _context.SaveChangesAsync();
		}

		private async Task<string> SaveFile(IFormFile file)
		{
			var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
			var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
			await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
			return fileName;
		}
	}
}
	