using Warehouse.Domain.Currency.Aggregates;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IInboundDocumentRepository
{
    Task<InboundDocument?> GetByIdAsync(Guid id);
    Task<IEnumerable<InboundDocument>> GetAllAsync();
    Task AddAsync(InboundDocument document);
    Task UpdateAsync(InboundDocument document);
    Task DeleteAsync(Guid id);
}