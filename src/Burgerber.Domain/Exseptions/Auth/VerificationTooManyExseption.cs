namespace Burgerber.Domain.Exseptions.Auth
{
    public class VerificationTooManyExseption : TooManyRequest
    {
        public VerificationTooManyExseption()
        {
            TitleMessage = "You tried more than limit";
        }
    }
}
