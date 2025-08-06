using Warehouse.Domain.Currency.Entities;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IBalanceRepository
{
    Task<Balance?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
    Task<IEnumerable<Balance>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Balance balance,CancellationToken cancellationToken = default);
    Task UpdateAsync(Balance balance,CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id,CancellationToken cancellationToken = default);
}