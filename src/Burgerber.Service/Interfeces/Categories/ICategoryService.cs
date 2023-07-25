using Burgerber.DataAccess.Utils;
using Burgerber.Domain.Entities.Catagories;
using Burgerber.Service.Dtos.Categories;

namespace Burgerber.Service.Interfeces.Categories
{
    public interface ICategoryService
    {
        public Task<bool> CreateAsync(CategoryCreateDto dto);

        public Task<bool> DeleteAsync(long categoryId);

        public Task<IList<Category>> GetAllAsync(PaginationParams @params);

        public Task<Category> GetByIdAsync(long categoryId);

        public Task<bool> UpdateAsync(long categoryId, CategoryUpdateDto dto);

        public Task<long> CountAsync();

    }
}
