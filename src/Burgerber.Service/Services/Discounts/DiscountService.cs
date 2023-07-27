using Burgerber.DataAccess.Interfaces.Discounts;
using Burgerber.DataAccess.Utils;
using Burgerber.Domain.Entities.Discounts;
using Burgerber.Domain.Exseptions.Discounts;
using Burgerber.Service.Commons.Helpers.TimeHelper;
using Burgerber.Service.Dtos.Discounts;
using Burgerber.Service.Interfeces.Common;
using Burgerber.Service.Interfeces.Discounts;

namespace Burgerber.Service.Services.Discounts;

public class DiscountService : IDiscountService
{
    private readonly IDiscountRepository _repository;
    private readonly IPaginator _paginator;

    public DiscountService(IDiscountRepository repository, IPaginator paginator)
    {
        this._repository = repository;
        this._paginator = paginator;
    }

    public async Task<long> CountAsync() => await _repository.CountAsync();


    public async Task<bool> CreatAsync(DiscountCreateDto dto)
    {
        Discount discount = new Discount()
        {
            Name = dto.Name,
            Description = dto.Description,
            CreateAt = TimeHelper.GetDateTime(),
            UpdateAt = TimeHelper.GetDateTime(),
        };
        var result = await _repository.CreateAsync(discount);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long discountId)
    {
        var result = await _repository.GetByIdAsync(discountId);
        if (result == null) throw new DiscountNotFoundExseption();

        var dbresult = await _repository.DeleteAsync(discountId);
        return dbresult > 0;
    }

    public async Task<IList<Discount>> GetAllAsync(PaginationParams @params)
    {
        var categories = await _repository.GetAllAsync(@params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);
        return categories;
    }

    public async Task<Discount> GetByIdAsync(long discountId)
    {
        var discount = await _repository.GetByIdAsync(discountId);
        if (discount is null) throw new DiscountNotFoundExseption();
        return discount;
    }

    public async Task<bool> UpdateAsync(long discountId, DiscountUpdateDto dto)
    {
        var discount = await _repository.GetByIdAsync(discountId);
        if (discount is null) throw new DiscountNotFoundExseption();

        discount.Name = dto.Name;
        discount.Description = dto.Description;
        discount.UpdateAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(discountId, discount);
        return dbResult > 0;
    }
}
