using Warehouse.Domain.Currency.Entities;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IInboundResourceRepository
{
    Task<IReadOnlyList<InboundResource>> GetByInboundDocumentIdAsync(Guid inboundDocumentId,CancellationToken cancellationToken = default);
    Task<IReadOnlyList<InboundResource>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Guid inboundDocumentId,InboundResource resource,CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid inboundDocumentId,InboundResource resource,CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id,CancellationToken cancellationToken = default);
}