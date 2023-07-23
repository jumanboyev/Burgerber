using Burgerber.DataAccess.Interfaces.Clients;
using Burgerber.Domain.Exseptions.Clients;
using Burgerber.Service.Commons.Helpers.TimeHelper;
using Burgerber.Service.Dtos.Auth;
using Burgerber.Service.Dtos.Notifications;
using Burgerber.Service.Dtos.Security;
using Burgerber.Service.Interfeces.Auth;
using Burgerber.Service.Interfeces.Notifications;
using Burgerber.Service.Services.Notifications;
using Microsoft.AspNetCore.Http;
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
    private ISmsSender _smsSender;
    private const int CACHED_MINUTES_FOR_REGISTER = 60;
    private const int CACHED_MINUTES_FOR_VERIFICATION = 5;

    public AuthService(IMemoryCache memoryCache , IClientRepository clientRepository,ISmsSender smsSender)
    {
        this._memoryCache=memoryCache;
        this._clientRepository=clientRepository;
        this._smsSender=smsSender;
    }
    public async Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterDto dto)
    {
        var result = await _clientRepository.GetByPhoneAsync(dto.PhoneNumber);
        if(result is not null) throw new ClientAlreadyExistsExseption(dto.PhoneNumber);

        //delete if exists client by this phone number
        if(_memoryCache.TryGetValue("register_"+dto.PhoneNumber, out RegisterDto cachedRegisterDto))
        {
            cachedRegisterDto.First_Name = cachedRegisterDto.First_Name;
            _memoryCache.Remove(dto.PhoneNumber);
        }
        else _memoryCache.Set("register_"+dto.PhoneNumber, dto , TimeSpan.FromMinutes(CACHED_MINUTES_FOR_REGISTER));

        return (Result: true, CachedMinutes: CACHED_MINUTES_FOR_REGISTER);

    }

    public async Task<(bool Result, int CashedVerificationMinutes)> SendCodeRegisterAsync(string phone)
    {
        if (_memoryCache.TryGetValue("register_"+phone, out RegisterDto registerDto))
        {
            VerificationDto verificationDto = new VerificationDto();
            verificationDto.Attempt = 0;
            verificationDto.CretaeAt = TimeHelper.GetDateTime();
            //make confirm code as random
            verificationDto.Code = 22222;

            if (_memoryCache.TryGetValue("verification_register_" + phone, out VerificationDto oldverificationDto))
            {
                _memoryCache.Remove("verification_register_" + phone);
            }
            _memoryCache.Set("verification_register_" + phone, verificationDto, 
                TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

            SmsMessange smsMessange = new SmsMessange();
            smsMessange.Recipent = phone.Substring(1);
            smsMessange.Content="Sizning tastiqlash ko'dingiz"+verificationDto.Code;
            smsMessange.Title = "Burgerber";

            var smsResult=await _smsSender.SendAsync(smsMessange);
            if (smsResult is true) return (Result: true, CachedVerificationMinutes: CACHED_MINUTES_FOR_VERIFICATION);
            else return (Result: false, CachedVerificationMinutes: 0);
        }
        else throw new ClientCacheDataExpiredExseption();
    }

    public Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, string code)
    {
        throw new NotImplementedException();
    }
}
