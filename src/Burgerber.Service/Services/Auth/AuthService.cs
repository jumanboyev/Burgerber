using Burgerber.DataAccess.Interfaces.Clients;
using Burgerber.Domain.Entities.Clients;
using Burgerber.Domain.Exseptions.Auth;
using Burgerber.Domain.Exseptions.Clients;
using Burgerber.Service.Commons.Helpers.TimeHelper;
using Burgerber.Service.Commons.Security;
using Burgerber.Service.Dtos.Auth;
using Burgerber.Service.Dtos.Notifications;
using Burgerber.Service.Dtos.Security;
using Burgerber.Service.Interfeces.Auth;
using Burgerber.Service.Interfeces.Notifications;
using Microsoft.Extensions.Caching.Memory;

namespace Burgerber.Service.Services.Auth;

public class AuthService : IAuthService
{
    private IMemoryCache _memoryCache;
    private IClientRepository _clientRepository;
    private ISmsSender _smsSender;
    private ITokenService _tokenService;
    private const int CACHED_MINUTES_FOR_REGISTER = 60;
    private const int CACHED_MINUTES_FOR_VERIFICATION = 5;
    public const string REGISTER_CACHE_KEY = "register_";
    public const string VERIFY_REGISTER_CACHE_KEY = "verification_register_";
    public const int VERIFICATION_MAX_ATTEMPT = 2;


    public AuthService(IMemoryCache memoryCache,
        IClientRepository clientRepository, 
        ISmsSender smsSender,
        ITokenService tokenService)
    {
        this._memoryCache = memoryCache;
        this._clientRepository = clientRepository;
        this._smsSender = smsSender;
        this._tokenService=tokenService;
    }
    public async Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterDto dto)
    {
        var result = await _clientRepository.GetByPhoneAsync(dto.PhoneNumber);
        if (result is not null) throw new ClientAlreadyExistsExseption(dto.PhoneNumber);

        //delete if exists client by this phone number
        if (_memoryCache.TryGetValue("register_" + dto.PhoneNumber, out RegisterDto cachedRegisterDto))
        {
            cachedRegisterDto.First_Name = cachedRegisterDto.First_Name;
            _memoryCache.Remove(dto.PhoneNumber);
        }
        else _memoryCache.Set("register_" + dto.PhoneNumber, dto, TimeSpan.FromMinutes(CACHED_MINUTES_FOR_REGISTER));

        return (Result: true, CachedMinutes: CACHED_MINUTES_FOR_REGISTER);

    }

    public async Task<(bool Result, int CashedVerificationMinutes)> SendCodeRegisterAsync(string phone)
    {
        if (_memoryCache.TryGetValue("register_" + phone, out RegisterDto registerDto))
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
            smsMessange.Content = "Sizning tastiqlash ko'dingiz" + verificationDto.Code;
            smsMessange.Title = "Burgerber";

            var smsResult = await _smsSender.SendAsync(smsMessange);
            if (smsResult is true) return (Result: true, CachedVerificationMinutes: CACHED_MINUTES_FOR_VERIFICATION);
            else return (Result: false, CachedVerificationMinutes: 0);
        }
        else throw new ClientCacheDataExpiredExseption();
    }

    public async Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, int code)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out RegisterDto registerDto))
        {
            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + phone, out VerificationDto verificationDto))
            {
                if (verificationDto.Attempt > VERIFICATION_MAX_ATTEMPT)
                {
                    throw new VerificationTooManyExseption();
                }
                else if (verificationDto.Code == code)
                {
                    var dbResult = await RegisterToDatabase(registerDto);
                    if(dbResult is true)
                    {
                        var client = await _clientRepository.GetByPhoneAsync(phone);
                        string token = await _tokenService.GenerateToken(client);
                        return (Result: true,Token: token);

                    }
                    else return (Result: false, Token: "");
                }
                else
                {
                    _memoryCache.Remove(REGISTER_CACHE_KEY + phone);
                    verificationDto.Attempt++;
                    _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phone, verificationDto,
                        TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));
                    return (Result: true, Token: "");
                }

            }
            else throw new VerificationCodeExpiredExseption();
        }
        else throw new ClientCacheDataExpiredExseption();

    }
    private async Task<bool> RegisterToDatabase(RegisterDto registerDto)
    {
        var client = new Client();
        client.FirstName = registerDto.First_Name;
        client.LastName = registerDto.Last_Name;
        client.PhoneNumber = registerDto.PhoneNumber;
        client.PhoneNumberConfirmed = true;

        var hasherResult = PasswordHasher.Hash(registerDto.Password);
        client.PasswordHash = hasherResult.hash;
        client.Salt = hasherResult.salt;

        client.CreateAt = client.UpdateAt = client.Birthdate = TimeHelper.GetDateTime();

        var dbResult = await _clientRepository.CreateAsync(client);
        return dbResult > 0;
    }

    public async Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto)
    {
        var client = await _clientRepository.GetByPhoneAsync(loginDto.PhoneNumber);
        if (client is null) throw new ClientsNotFoundExseption();

        var hasherResult=PasswordHasher.Verify(loginDto.Password, client.PasswordHash, client.Salt);
        if(hasherResult==false) throw new PasswordNotMatchExseption();

        var token =await _tokenService.GenerateToken(client);
        return (Result:true,Token:token);

    }
}
