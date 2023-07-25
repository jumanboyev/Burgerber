using Burgerber.DataAccess.Common.Interfaces;
using Burgerber.DataAccess.ViewModels.Clients;
using Burgerber.Domain.Entities.Clients;

namespace Burgerber.DataAccess.Interfaces.Clients;

public interface IClientRepository : IRepository<Client, ClientViewModel>, IGetAll<ClientViewModel>
    , ISearchable<ClientViewModel>
{
    public Task<Client?> GetByPhoneAsync(string phone);
}
