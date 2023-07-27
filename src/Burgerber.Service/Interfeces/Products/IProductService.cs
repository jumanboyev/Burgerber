using Burgerber.DataAccess.Utils;
using Burgerber.Domain.Entities.Products;
using Burgerber.Service.Dtos.Products;

namespace Burgerber.Service.Interfeces.Products;

public interface IProductService
{
    public Task<bool> CreateAsync(ProductCreateDto dto);

    public Task<bool> DeleteAsync(long  productId);

    public Task<IList<Product>> GetAllAsync(PaginationParams @params);

    public Task<Product> GetByIdAsync(long productId);

    public Task<bool> UpdateAsync(long productId, ProductUpdateDto dto);

    public Task<long> CountAsync();
}
