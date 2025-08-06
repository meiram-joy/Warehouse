using Warehouse.Domain.Currency.Aggregates;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IOutboundDocumentRepository
{
    Task<OutboundDocument?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
    Task<IEnumerable<OutboundDocument>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(OutboundDocument document,CancellationToken cancellationToken = default);
    Task UpdateAsync(OutboundDocument document,CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id,CancellationToken cancellationToken = default);
}