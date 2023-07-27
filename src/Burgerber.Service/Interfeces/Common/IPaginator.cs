using Burgerber.DataAccess.Utils;

namespace Burgerber.Service.Interfeces.Common;

public interface IPaginator
{
    public void Paginate(long itemsCount, PaginationParams @params);
}
