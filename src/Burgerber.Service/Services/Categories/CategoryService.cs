using Burgerber.DataAccess.Interfaces.Categories;
using Burgerber.DataAccess.Utils;
using Burgerber.Domain.Entities.Catagories;
using Burgerber.Domain.Exseptions.Categories;
using Burgerber.Service.Commons.Helpers.TimeHelper;
using Burgerber.Service.Dtos.Categories;
using Burgerber.Service.Interfeces.Categories;

namespace Burgerber.Service.Services.Categories;

public class CategoryService : ICategoryService
{
    private ICategoryRepository _repository;

    public CategoryService(ICategoryRepository categoryRepository)
    {

        this._repository = categoryRepository;
    }

    public async Task<long> CountAsync()
    {
        var res = await _repository.CountAsync();
        return res;
    }

    public async Task<bool> CreateAsync(CategoryCreateDto dto)
    {
        Category category = new Category()
        {
            Name = dto.Name,
            Description = dto.Description,
            CreateAt = TimeHelper.GetDateTime(),
            UpdateAt = TimeHelper.GetDateTime()
        };

        var res = await _repository.CreateAsync(category);
        return res > 0;
    }

    public async Task<bool> DeleteAsync(long categoryId)
    {
        /*var category = await _repository.GetByIdAsync(categoryId);
       if (category is null) throw new CategoryNotFoundExseption();*/

        var res = await _repository.DeleteAsync(categoryId);
        return res > 0;
    }

    public async Task<IList<Category>> GetAllAsync(PaginationParams @params)
    {
        var res = await _repository.GetAllAsync(@params);
        return res;
    }

    public async Task<Category> GetByIdAsync(long categoryId)
    {
        var res = await _repository.GetByIdAsync(categoryId);
        if (res is null) throw new CategoryNotFoundExseption();
        return res;
    }

    public async Task<bool> UpdateAsync(long categoryId, CategoryUpdateDto dto)
    {
        var res = await _repository.GetByIdAsync(categoryId);
        if (res is null) throw new CategoryNotFoundExseption();
        
        res.Name=dto.Name;
        res.Description=dto.Description;
        res.UpdateAt= TimeHelper.GetDateTime();

        var result = await _repository.UpdateAsync(categoryId, res);
        return result > 0;
    }

}
