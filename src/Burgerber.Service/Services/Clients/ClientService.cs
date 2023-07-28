using Burgerber.DataAccess.Interfaces.Clients;
using Burgerber.DataAccess.Utils;
using Burgerber.Domain.Entities.Clients;
using Burgerber.Domain.Exseptions.Clients;
using Burgerber.Service.Commons.Helpers.TimeHelper;
using Burgerber.Service.Dtos.Clients;
using System.Collections.Generic;

namespace Burgerber.Service.Services.Clients;

public class ClientService
{
    private IClientRepository _clientRepository;

    public ClientService(IClientRepository userRepository)
    {
        this._clientRepository = userRepository;
    }
    public async Task<long> CountAsync()
    {
        var result = await _clientRepository.CountAsync();
        return result;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var resfound = await _clientRepository.GetByIdAsync(id);
        if (resfound is null)
        {
            throw new ClientsNotFoundExseption();
        }
        var result = await _clientRepository.DeleteAsync(id);
        if (result > 0) return true;
        else return false;
    }

    public async Task<List<Client>> GetAllAsync(PaginationParams PaginationParams)
    {
        var result = await _clientRepository.GetAllAsync(PaginationParams);
        if (result is null)
        {
            throw new ClientsNotFoundExseption();
        }
        return result.ToList();
    }

    public async Task<Client> GetByIdAsync(long id)
    {
        var result = await _clientRepository.GetByIdAsync(id);
        if (result is null)
        {
            throw new ClientsNotFoundExseption();
        }
        return result;
    }

    //public async Task<bool> UpdateAsync(long id, RegisterClientDto registerUserDto)
    //{
    //    var resfound = await _clientRepository.GetByIdAsync(id);
    //    if (resfound is null)
    //    {
    //        throw new ClientsNotFoundExseption();
    //    }
    //    Client user = new Client();
    //    user.FirstName = registerUserDto.FirstName;
    //    user.LastName = registerUserDto.LastName;
    //    user.PhoneNumber = registerUserDto.PhoneNumber;
    //    user.Birthdate = registerUserDto.b;
    //    user.MiddleName = registerUserDto.MiddleName;
    //    user.IsMale = registerUserDto.IsMale;
    //    user.IdentityRole = UserRole.User;
    //    user.WasBorn = registerUserDto.WasBorn;
    //    user.UpdatedAt = TimeHelper.GetDateTime();
    //    //user.ImagePath = registerUserDto.ImagePath.ToString();
    //    var hasherResult = PasswordHasher.Hash(registerUserDto.Password);
    //    user.PasswordHash = hasherResult.Hash;
    //    user.Salt = hasherResult.Salt;

    //    var dbresult = await _userRepository.UpdateAsync(id, user);
    //    return dbresult > 0;
    //}
}
