using Warehouse.Domain.Currency.Entities;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IMeasurementUnitRepository
{
    Task<UnitOfMeasurement?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
    Task<IEnumerable<UnitOfMeasurement>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(UnitOfMeasurement unit,CancellationToken cancellationToken = default);
    Task UpdateAsync(UnitOfMeasurement unit,CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id,CancellationToken cancellationToken = default);
}