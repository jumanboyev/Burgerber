using Burgerber.Service.Dtos.Auth;

namespace Burgerber.Service.Interfeces.Auth;

public interface IAuthService
{
    public Task<(bool Result,int CachedMinutes)> RegisterAsync(RegisterDto dto);

    public Task<(bool Result,int CashedVerificationMinutes)> SendCodeRegisterAsync(string phone);

    public Task<(bool Result,string Token)> VerifyRegisterAsync(string phone,string code);
}
