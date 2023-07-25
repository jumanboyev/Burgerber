namespace Burgerber.Domain.Exseptions.Auth
{
    public class PasswordNotMatchExseption:BadRequestExseption
    {
        public PasswordNotMatchExseption()
        {
            TitleMessage = "Password is invalid";
        }
    }
}
