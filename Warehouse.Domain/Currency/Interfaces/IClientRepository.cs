using Warehouse.Domain.Currency.Entities;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
    Task<IEnumerable<Client>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Client client,CancellationToken cancellationToken = default);
    Task UpdateAsync(Client client,CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id,CancellationToken cancellationToken = default);
}