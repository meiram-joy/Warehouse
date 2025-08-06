using Warehouse.Domain.Currency.Entities;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Infrastructure.Repositories;

public class MeasurementUnitRepository : IMeasurementUnitRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    
    public MeasurementUnitRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public Task<UnitOfMeasurement?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UnitOfMeasurement>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(UnitOfMeasurement unit)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UnitOfMeasurement unit)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}