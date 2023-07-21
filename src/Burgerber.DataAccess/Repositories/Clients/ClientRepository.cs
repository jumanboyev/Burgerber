using Burgerber.DataAccess.Interfaces.Clients;
using Burgerber.DataAccess.Utils;
using Burgerber.DataAccess.ViewModels.Clients;
using Burgerber.Domain.Entities.Clients;
using Dapper;

namespace Burgerber.DataAccess.Repositories.Clients;

public class ClientRepository :BaseRepository, IClientRepository
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
            string query= "INSERT INTO public.clients(first_name, last_name, phone_number, phone_number_confirmed, password_hash, salt, create_at, update_at, birthdate) " +
                "VALUES (@FirstName, @LastName, @PhoneNumber, @PhoneNumberConfirmed, @PasswordHash, @Salt, CreateAt, @UpdateAt, @Birthdate);";

            var result=await _connection.ExecuteAsync(query,entity);
            return result;
        }
        catch 
        {

            return 0;
        }
        finally
        {
            await  _connection.CloseAsync();
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

    public Task<IList<ClientViewModel>> GetAllAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<ClientViewModel> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
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
            await _connection.CloseAsync() ;
        }
    }

    public Task<(int ItemsCount, List<ClientViewModel>)> SearchableAsync(string query, PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(long id, Client entity)
    {
        throw new NotImplementedException();
    }
}
