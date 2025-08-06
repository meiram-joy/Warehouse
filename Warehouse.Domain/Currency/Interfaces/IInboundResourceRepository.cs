using Warehouse.Domain.Currency.Entities;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IInboundResourceRepository
{
    Task<InboundResource?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
    Task<IEnumerable<InboundResource>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(InboundResource resource,CancellationToken cancellationToken = default);
    Task UpdateAsync(InboundResource resource,CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id,CancellationToken cancellationToken = default);
}