namespace Burgerber.Domain.Exseptions.Clients;

public class ClientsNotFoundExseption:NotFoundExseption
{
    public ClientsNotFoundExseption()
    {
        this.TitleMessage = "Client Not Found";
    }
}
