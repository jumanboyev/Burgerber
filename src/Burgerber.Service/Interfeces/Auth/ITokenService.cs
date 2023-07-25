using Burgerber.Domain.Entities.Clients;

namespace Burgerber.Service.Interfeces.Auth;

public interface ITokenService
{
    public Task<string> GenerateToken(Client client);
}
