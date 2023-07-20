using Burgerber.DataAccess.Interfaces.Categories;
using Burgerber.DataAccess.Utils;
using Burgerber.Domain.Entities.Catagories;
using Dapper;
using static Dapper.SqlMapper;

namespace Burgerber.DataAccess.Repositories.Categories;

public class CategoryRepository : BaseRepository, ICategoryRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select count(*) from categories";

            var result = await _connection.QuerySingleAsync<long>(query);
            return result;
        }
        catch
        {

            return 0;
        }
        finally { await _connection.CloseAsync(); }
    }

    public async Task<int> CreateAsync(Category entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.categories( name, description, create_at, update_at)" +
                "VALUES ( @Name, @Description, @CreateAt, @UpdateAt);";

            var result = await _connection.ExecuteAsync(query, entity);
            return result;
        }
        catch
        {

            return 0;
        }
        finally { await _connection.CloseAsync(); }
    }

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Delete from categories where id=@Id";

            var result = await _connection.ExecuteAsync(query, new { Id = id });
            return result;
        }
        catch
        {

            return 0;
        }
        finally { await _connection.CloseAsync(); }
    }

    public async Task<IList<Category>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select * from categories order by id desc " +
                $" offset {@params.SkipCount} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<Category>(query)).ToList();
            return result;
        }
        catch
        {

            return new List<Category>();
        }
        finally { await _connection.CloseAsync(); }
    }

    public async Task<Category> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select * from categories where id=@Id";

            var result = await _connection.QuerySingleAsync<Category>(query, new { Id = id });
            return result;
        }
        catch
        {
            return null;
        }
        finally { await _connection.CloseAsync(); }
    }

    public async Task<int> UpdateAsync(long id, Category entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE public.categories SET name=@Name, description=@Description, create_at=@CreateAt, update_at=@UpdateAt " +
                $"WHERE id={id};";

            var result = await _connection.ExecuteAsync(query, entity);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
}
