using Burgerber.DataAccess.Common.Interfaces;
using Burgerber.DataAccess.ViewModels.Clients;
using Burgerber.Domain.Entities.Clients;

namespace Burgerber.DataAccess.Interfaces.Clients;

public interface IClientRepository : IRepository<Client, Client>, IGetAll<Client>
   
{
    public Task<Client?> GetByPhoneAsync(string phone);
}
