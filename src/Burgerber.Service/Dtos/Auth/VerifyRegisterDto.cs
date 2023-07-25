namespace Burgerber.Service.Dtos.Auth;

public class VerifyRegisterDto
{
    public string PhoneNumber { get; set; } = string.Empty;
    public int Code { get; set; }

}
