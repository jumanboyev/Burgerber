using Burgerber.DataAccess.Utils;

namespace Burgerber.DataAccess.Common.Interfaces
{
    public interface ISearchable<TModel>
    {
        public Task<(int ItemsCount, List<TModel>)> SearchableAsync(string query, PaginationParams @params);
    }
}
