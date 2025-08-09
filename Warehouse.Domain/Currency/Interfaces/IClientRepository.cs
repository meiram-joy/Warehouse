using Warehouse.Domain.Currency.Entities;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IClientRepository
{
    Task<Client?> GetByNameAndAddressAsync(string name,string address,CancellationToken cancellationToken = default);
    Task<(Client? clientToCreate, bool nameExists)> GetForCreateCheckAsync(string name,string address, CancellationToken cancellationToken);

    Task<Client?> GetByIdAsync(Guid Id,CancellationToken cancellationToken = default);
    Task<IEnumerable<Client>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Client client,CancellationToken cancellationToken = default);
    Task UpdateAsync(Client client,CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id,CancellationToken cancellationToken = default);
    Task<bool> IsClientUsedAsync(Guid clientId,CancellationToken cancellationToken = default);
}