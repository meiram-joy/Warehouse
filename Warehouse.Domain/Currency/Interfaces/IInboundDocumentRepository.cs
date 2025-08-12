using Warehouse.Domain.Currency.Aggregates;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IInboundDocumentRepository
{
    Task<InboundDocument?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
    Task<InboundDocument?> GetByDocumentNumberAsync(string documentNumber,CancellationToken cancellationToken = default);
    Task<IReadOnlyList<InboundDocument>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(InboundDocument document,CancellationToken cancellationToken = default);
    Task UpdateAsync(InboundDocument document,CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id,CancellationToken cancellationToken = default);
    Task<(InboundDocument? documentToUpdate, bool numberExists)> GetForUpdateCheckAsync(Guid id, string documentNumber, CancellationToken cancellationToken);

}