using Burgerber.DataAccess.Interfaces;
using Burgerber.DataAccess.Interfaces.Products;
using Burgerber.DataAccess.Utils;
using Burgerber.Domain.Entities.Products;
using Burgerber.Service.Commons.Helpers.TimeHelper;
using Burgerber.Service.Dtos.Products;
using Burgerber.Service.Interfeces.Common;
using Burgerber.Service.Interfeces.Products;
using Microsoft.Extensions.Caching.Memory;

namespace Burgerber.Service.Services.Products;

public class ProductService : IProductService
{
    private IProductRepository _productRepository;
    private IFileService _fileservice;
    private IMemoryCache _memoryCache;

    public ProductService(IProductRepository productRepository,
        IFileService fileService,IMemoryCache memoryCache)
    {
        this._productRepository=productRepository;
        this._fileservice=fileService;
        this._memoryCache = memoryCache;
    }
    public async Task<long> CountAsync()
    {
        return await _productRepository.CountAsync();
    }

    public async Task<bool> CreateAsync(ProductCreateDto dto)
    {
        string imagePath = await _fileservice.UploadImageAsync(dto.Image);

        Product product = new Product()
        {
            CategoryId = dto.CategoryId,
            ImagePath = imagePath,
            Name = dto.Name,
            UnitPrice = dto.UnitPrice,
            Description = dto.Description,
            CreateAt = TimeHelper.GetDateTime(),
            UpdateAt= TimeHelper.GetDateTime()
        };
        return await _productRepository.CreateAsync(product)>0;
    }

    public async Task<bool> DeleteAsync(long productId)
    {
        return await _productRepository.DeleteAsync(productId)>0;
    }

    public async Task<IList<Product>> GetAllAsync(PaginationParams @params)
    {
        var res = await _productRepository.GetAllAsync(@params);
        return res;
    }

    public async Task<Product> GetByIdAsync(long productId)
    {
        return await _productRepository.GetByIdAsync(productId);
    }

    public async Task<bool> UpdateAsync(long productId, ProductUpdateDto dto)
    {
        var resultGetbyId = await _productRepository.GetByIdAsync(productId);
        if (resultGetbyId is not null)
        {
            resultGetbyId.Name = dto.Name;
            resultGetbyId.Description = dto.Description;
            resultGetbyId.UnitPrice = dto.UnitPrice;
            resultGetbyId.CategoryId = dto.CategoryId;
            if (resultGetbyId.ImagePath is not null)
            {
                await _fileservice.DeleteImageAsync(resultGetbyId.ImagePath);
                string newIMG = await _fileservice.UploadImageAsync(dto.Image);
                resultGetbyId.ImagePath = newIMG;
                resultGetbyId.UpdateAt = TimeHelper.GetDateTime();
            }
        }
        var result = await _productRepository.UpdateAsync(productId, resultGetbyId);
        return result > 0;
    }
}
