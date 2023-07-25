namespace Burgerber.Domain.Exseptions.Clients;

public class ClientAlreadyExistsExseption : AlreadyExistsExseption
{
    public ClientAlreadyExistsExseption()
    {
        this.TitleMessage = "Client already exists";
    }
    public ClientAlreadyExistsExseption(string phone)
    {
        this.TitleMessage = "This is phonenumber already registered";
    }
}
