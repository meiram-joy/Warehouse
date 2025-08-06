using Warehouse.Domain.Currency.Entities;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IResourceRepository
{
    Task<Resource?> GetByIdAsync(Guid id);
    Task<IEnumerable<Resource>> GetAllAsync();
    Task AddAsync(Resource resource);
    Task UpdateAsync(Resource resource);
    Task DeleteAsync(Guid id);
}