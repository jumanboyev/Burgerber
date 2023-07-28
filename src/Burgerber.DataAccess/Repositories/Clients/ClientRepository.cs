using Burgerber.DataAccess.Interfaces.Clients;
using Burgerber.DataAccess.Utils;
using Burgerber.DataAccess.ViewModels.Clients;
using Burgerber.Domain.Entities.Clients;
using Dapper;

namespace Burgerber.DataAccess.Repositories.Clients;

public class ClientRepository : BaseRepository, IClientRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select count(*) from clients";

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

    public async Task<int> CreateAsync(Client entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.clients(first_name, last_name, phone_number, phone_number_confirmed, password_hash, salt, create_at, update_at, birthdate) " +
                "VALUES (@FirstName, @LastName, @PhoneNumber, @PhoneNumberConfirmed, @PasswordHash, @Salt, @CreateAt, @UpdateAt, @Birthdate);";

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
            string query = $"Delete From clients where id={id}";

            return await _connection.ExecuteAsync(query);
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

    public async Task<IList<Client>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM public.clients ORDER BY id DESC " +
                $"OFFSET {@params.SkipCount} LIMIT {@params.PageSize}";
            var result = (await _connection.QueryAsync<Client>(query)).ToList();
            return result;
        }
        catch
        {
            IList<Client> result = new List<Client>();
            return result;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Client?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select * FROM public.clients where id = {id}";
            var data = await _connection.QuerySingleAsync<Client>(query, new { Id = id });
            return data;
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

    public async Task<Client?> GetByPhoneAsync(string phone)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select * from clients where phone_number=@PhoneNumber";

            var data = await _connection.QuerySingleAsync<Client>(query, new { PhoneNumber = phone });
            return data;
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

   

    public async Task<int> UpdateAsync(long id, Client entity)
    {
        try
        { 
            await _connection.OpenAsync();
            string query = "UPDATE public.clients SET  first_name=@FirstName, last_name=@LastName, phone_number=@PhoneNumber, " +
                "phone_number_confirmed=@PhoneNumberConfirmed, password_hash=@PasswordHash, salt=@Salt," +
                " create_at=@CreateAt, update_at=@UpdateAt, birthdate=@Birthdate" +
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
