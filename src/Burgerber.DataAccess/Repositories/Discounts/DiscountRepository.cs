using Burgerber.DataAccess.Interfaces.Discounts;
using Burgerber.DataAccess.Utils;
using Burgerber.Domain.Entities.Discounts;
using Dapper;

namespace Burgerber.DataAccess.Repositories.Discounts;

public class DiscountRepository :BaseRepository, IDiscountRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"SELECT count(*) FROM discounts";

            var result = await _connection.QuerySingleAsync<long>(query);
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

    public async Task<int> CreateAsync(Discount entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO discounts( name, create_at, update_at, description) " +
                "VALUES (@Name, @CreateAt, @UpdateAt, @Description);";
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

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "DELETE FROM discounts WHERE id = @Id";
            var result = await _connection.ExecuteAsync(query, new { Id = id });
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

    public async Task<IList<Discount>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"SELECT * FROM discounts order by id desc " +
                $"offset {@params.SkipCount} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<Discount>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<Discount>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Discount?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM discounts where id=@Id";
            var result = await _connection.QuerySingleAsync<Discount>(query, new { Id = id });
            return result;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, Discount entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"UPDATE public.discounts " +
                $"SET name=@Name, description=@Description, created_at=@CreatedAt, updated_at=@UpdatedAt " +
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
