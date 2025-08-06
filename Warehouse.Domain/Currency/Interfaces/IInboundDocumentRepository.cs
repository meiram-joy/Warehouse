using Warehouse.Domain.Currency.Aggregates;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IInboundDocumentRepository
{
    Task<InboundDocument?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
    Task<IEnumerable<InboundDocument>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(InboundDocument document,CancellationToken cancellationToken = default);
    Task UpdateAsync(InboundDocument document,CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id,CancellationToken cancellationToken = default);
}