using Burgerber.DataAccess.Interfaces.Clients;
using Burgerber.Domain.Exseptions.Clients;
using Burgerber.Service.Dtos.Auth;
using Burgerber.Service.Interfeces.Auth;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerber.Service.Services.Auth;

public class AuthService : IAuthService
{
    private IMemoryCache _memoryCache;
    private IClientRepository _clientRepository;
    private const int CACHED_MINUTES_FOR_REGISTER = 60;
    private const int CACHED_MINUTES_FOR_VERIFICATION = 5;

    public AuthService(IMemoryCache memoryCache , IClientRepository clientRepository)
    {
        this._memoryCache=memoryCache;
        this._clientRepository=clientRepository;
    }
    public async Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterDto dto)
    {
        var result = await _clientRepository.GetByPhoneAsync(dto.PhoneNumber);
        if(result is not null) throw new ClientAlreadyExistsExseption(dto.PhoneNumber);

        //delete if exists client by this phone number
        if(_memoryCache.TryGetValue(dto.PhoneNumber, out RegisterDto cachedRegisterDto))
        {
            cachedRegisterDto.First_Name = cachedRegisterDto.First_Name;
            _memoryCache.Remove(dto.PhoneNumber);
        }
        else _memoryCache.Set(dto.PhoneNumber, dto , TimeSpan.FromMinutes(CACHED_MINUTES_FOR_REGISTER));

        return (Result: true, CachedMinutes: CACHED_MINUTES_FOR_REGISTER);

    }

    public Task<(bool Result, int CashedVerificationMinutes)> SendCodeRegisterAsync(string phone)
    {
        throw new NotImplementedException();
    }

    public Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, string code)
    {
        throw new NotImplementedException();
    }
}
