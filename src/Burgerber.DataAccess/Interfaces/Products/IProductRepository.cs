using Burgerber.DataAccess.Common.Interfaces;
using Burgerber.DataAccess.ViewModels.Products;
using Burgerber.Domain.Entities.Products;

namespace Burgerber.DataAccess.Interfaces.Products;

public interface IProductRepository:IRepository<Product,Product>,IGetAll<Product>,
    ISearchable<ProductViewModel>
{

}
