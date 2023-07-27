using Burgerber.DataAccess.Interfaces.Products;
using Burgerber.DataAccess.Utils;
using Burgerber.DataAccess.ViewModels.Products;
using Burgerber.Domain.Entities.Catagories;
using Burgerber.Domain.Entities.Products;
using Dapper;

namespace Burgerber.DataAccess.Repositories.Products;

public class ProductRepository :BaseRepository, IProductRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select count(*) from products";

            return await _connection.QuerySingleAsync<long>(query);
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

    public async Task<int> CreateAsync(Product entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.products(category_id, unit_price, name, image_path, create_at, update_at, description) " +
                "VALUES (@CategoryId, @UnitPrice, @Name, @ImagePath, @CreateAt, @UpdateAt, @Description);";

            return await _connection.ExecuteAsync(query, entity);
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
            string query = "Delete from products where id=@Id";

            return await _connection.ExecuteAsync(query, new {Id=id});
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

    public async Task<IList<Product>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select * from products order by id desc " +
                $" offset {@params.SkipCount} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<Product>(query)).ToList();
            return result;
        }
        catch
        {

            return new List<Product>();
        }
        finally { await _connection.CloseAsync(); }
    }

    public async Task<Product?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM products where id=@Id";
            var result = await _connection.QuerySingleAsync<Product>(query, new { Id = id });
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

    public Task<(int ItemsCount, List<ProductViewModel>)> SearchableAsync(string query, PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public async Task<int> UpdateAsync(long id, Product entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE public.products " +
                            "SET  name = @Name, description = @Description, image_path = @ImagePath, unit_price = @Unitprice,  update_at = @UpdateAt" +
                           $" WHERE id = {id};";
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
