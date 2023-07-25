namespace Burgerber.Domain.Exseptions.Clients;

public class ClientCacheDataExpiredExseption : ExpiredExseption
{
    public ClientCacheDataExpiredExseption()
    {
        TitleMessage = "Client data has expired";
    }
}
