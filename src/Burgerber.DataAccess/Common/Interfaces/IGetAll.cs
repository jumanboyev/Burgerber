using Burgerber.DataAccess.Utils;

namespace Burgerber.DataAccess.Common.Interfaces;

public interface IGetAll<TModal>
{
    public Task<IList<TModal>> GetAllAsync(PaginationParams @params);

}
