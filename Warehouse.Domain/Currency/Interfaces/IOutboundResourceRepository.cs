using Warehouse.Domain.Currency.Entities;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IOutboundResourceRepository
{
    Task<OutboundResource?> GetByIdAsync(Guid id);
    Task<IEnumerable<OutboundResource>> GetAllAsync();
    Task AddAsync(OutboundResource resource);
    Task UpdateAsync(OutboundResource resource);
    Task DeleteAsync(Guid id);
}