using Warehouse.Domain.Currency.Entities;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IMeasurementUnitRepository
{
    Task<UnitOfMeasurement?> GetByIdAsync(Guid id);
    Task<IEnumerable<UnitOfMeasurement>> GetAllAsync();
    Task AddAsync(UnitOfMeasurement unit);
    Task UpdateAsync(UnitOfMeasurement unit);
    Task DeleteAsync(Guid id);
}