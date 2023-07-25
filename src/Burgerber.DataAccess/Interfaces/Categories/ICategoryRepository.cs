using Burgerber.DataAccess.Common.Interfaces;
using Burgerber.Domain.Entities.Catagories;

namespace Burgerber.DataAccess.Interfaces.Categories;

public interface ICategoryRepository : IRepository<Category, Category>,
    IGetAll<Category>
{

}
