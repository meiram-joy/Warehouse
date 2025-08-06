using Dapper;
using Warehouse.Domain.Currency.Entities;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Infrastructure.Repositories;

public class MeasurementUnitRepository : IMeasurementUnitRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    
    public MeasurementUnitRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        EnsureTableCreated();
    }

    public async Task<UnitOfMeasurement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"SELECT * FROM UnitOfMeasurement WHERE Id = @Id";
        var unit = await connection.QuerySingleOrDefaultAsync<UnitOfMeasurement>(sql, new { Id = id });
        if (unit == null) return null;
        
        return unit;
    }

    public async Task<IEnumerable<UnitOfMeasurement>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"SELECT * FROM UnitOfMeasurement";
        var unit = await connection.QueryAsync<UnitOfMeasurement>(sql);
        return unit;
    }

    public async Task AddAsync(UnitOfMeasurement unit, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"INSERT INTO UnitOfMeasurement (Id, UnitName, Status) 
                             VALUES (@Id, @UnitName, @Status)";
        await connection.ExecuteAsync(sql, new
        {
            Id = unit.ID,
            UnitName = unit.UnitName,
            Status = unit.Status
        });
    }

    public async Task UpdateAsync(UnitOfMeasurement unit, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"UPDATE UnitOfMeasurement 
                             SET UnitName = @UnitName, Status = @Status 
                             WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new
        {
            Id = unit.ID,
            UnitName = unit.UnitName,
            Status = unit.Status
        });
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"DELETE FROM UnitOfMeasurement WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }
    
    private void EnsureTableCreated()
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();
        var createTableSql = @"
            CREATE TABLE IF NOT EXISTS UnitOfMeasurement (
            Id TEXT PRIMARY KEY,
            UnitName TEXT NOT NULL,
            Status INTEGER NOT NULL
            );
        ";
        using var cmd = connection.CreateCommand();
        cmd.CommandText = createTableSql;
        cmd.ExecuteNonQuery();
    }
}