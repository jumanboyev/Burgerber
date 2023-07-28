using Burgerber.DataAccess.Utils;
using Burgerber.Domain.Entities.Clients;
using Burgerber.Service.Dtos.Clients;

namespace Burgerber.Service.Interfeces.Clients;

public interface IUserService
{
    public Task<Client?> GetByIdAsync(long id);
    public Task<List<Client>> GetAllAsync(PaginationParams PaginationParams);
    public Task<bool> UpdateAsync(long id, RegisterClientDto dto);
    public Task<bool> DeleteAsync(long id);
    public Task<long> CountAsync();
}
