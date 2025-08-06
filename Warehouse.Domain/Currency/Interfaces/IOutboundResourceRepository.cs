using Warehouse.Domain.Currency.Entities;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IOutboundResourceRepository
{
    Task<OutboundResource?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
    Task<IEnumerable<OutboundResource>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(OutboundResource resource,CancellationToken cancellationToken = default);
    Task UpdateAsync(OutboundResource resource,CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id,CancellationToken cancellationToken = default);
}