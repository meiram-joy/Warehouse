using Warehouse.Domain.Currency.Entities;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IInboundResourceRepository
{
    Task<InboundResource?> GetByIdAsync(Guid id);
    Task<IEnumerable<InboundResource>> GetAllAsync();
    Task AddAsync(InboundResource resource);
    Task UpdateAsync(InboundResource resource);
    Task DeleteAsync(Guid id);
}