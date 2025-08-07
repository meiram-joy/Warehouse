using Warehouse.Domain.Currency.Entities;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IResourceRepository
{
    Task<Resource?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
    Task<Resource?> GetByNameAsync(string resourceName, CancellationToken cancellationToken = default);
    Task<IEnumerable<Resource>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Resource resource,CancellationToken cancellationToken = default);
    Task UpdateAsync(Resource resource,CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id,CancellationToken cancellationToken = default);
    Task<bool> IsClientUsedAsync(Guid resourceId,CancellationToken cancellationToken = default);
}