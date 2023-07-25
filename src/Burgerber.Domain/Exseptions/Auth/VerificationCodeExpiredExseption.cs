namespace Burgerber.Domain.Exseptions.Auth;

public class VerificationCodeExpiredExseption : ExpiredExseption
{
    public VerificationCodeExpiredExseption()
    {
        TitleMessage = "Verification code is Expired";
    }
}
