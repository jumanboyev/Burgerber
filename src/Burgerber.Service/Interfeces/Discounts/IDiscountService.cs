using Burgerber.DataAccess.Utils;
using Burgerber.Domain.Entities.Discounts;
using Burgerber.Service.Dtos.Discounts;

namespace Burgerber.Service.Interfeces.Discounts;

public interface IDiscountService
{
    public Task<bool> CreatAsync(DiscountCreateDto dto);

    public Task<bool> DeleteAsync(long discountId);

    public Task<long> CountAsync();

    public Task<IList<Discount>> GetAllAsync(PaginationParams @params);

    public Task<Discount> GetByIdAsync(long discountId);

    public Task<bool> UpdateAsync(long discountId, DiscountUpdateDto dto);
}
