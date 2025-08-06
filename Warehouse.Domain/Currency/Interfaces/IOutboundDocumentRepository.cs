using Warehouse.Domain.Currency.Aggregates;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IOutboundDocumentRepository
{
    Task<OutboundDocument?> GetByIdAsync(Guid id);
    Task<IEnumerable<OutboundDocument>> GetAllAsync();
    Task AddAsync(OutboundDocument document);
    Task UpdateAsync(OutboundDocument document);
    Task DeleteAsync(Guid id);
}