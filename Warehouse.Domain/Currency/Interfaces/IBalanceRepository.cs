using Warehouse.Domain.Currency.Entities;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IBalanceRepository
{
    Task<Balance?> GetByResourceIdAndUnitIdAsync(Guid resourceId,Guid unitId,CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Balance>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Balance balance,CancellationToken cancellationToken = default);
    Task UpdateAsync(Balance balance,CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id,CancellationToken cancellationToken = default);
}