using Burgerber.DataAccess.Common.Interfaces;
using Burgerber.Domain.Entities.Discounts;

namespace Burgerber.DataAccess.Interfaces.Discounts;

public interface IDiscountRepository : IRepository<Discount, Discount>, IGetAll<Discount>
{

}
