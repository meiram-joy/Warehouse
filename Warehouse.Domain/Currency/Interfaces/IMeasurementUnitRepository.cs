using Warehouse.Domain.Currency.Entities;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IMeasurementUnitRepository
{
    Task<UnitOfMeasurement?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
    Task<UnitOfMeasurement?> GetByNameAsync(string unitName,CancellationToken cancellationToken = default);
    Task<bool> GetForCreateCheckAsync(string unitName, CancellationToken cancellationToken = default);
    Task<(UnitOfMeasurement? existingUnit, bool nameExists)> GetForUpdateCheckAsync(string unitName, CancellationToken cancellationToken = default);
    Task<IEnumerable<UnitOfMeasurement>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(UnitOfMeasurement unit,CancellationToken cancellationToken = default);
    Task UpdateAsync(UnitOfMeasurement unit,CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id,CancellationToken cancellationToken = default);
    Task<bool> IsClientUsedAsync(Guid unitId,CancellationToken cancellationToken = default);
}