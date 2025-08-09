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

    public async Task<UnitOfMeasurement?> GetByNameAsync(string unitName, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"SELECT * FROM UnitOfMeasurement WHERE UnitName = @UnitName";
        var unit = await connection.QuerySingleOrDefaultAsync<UnitOfMeasurement>(sql, new { UnitName = unitName });
        if (unit == null) return null;
        
        return unit;
    }
    public async Task<(UnitOfMeasurement? existingUnit, bool nameExists)> GetForCreateCheckAsync(string unitName, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        var sql = @"
        SELECT * 
        FROM UnitOfMeasurement 
        WHERE UnitName = @UnitName;

        SELECT CASE WHEN EXISTS (
            SELECT 1 
            FROM UnitOfMeasurement 
            WHERE UnitName = @UnitName
        )
        THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END;
    ";

        using var multi = await connection.QueryMultipleAsync(sql, new
        {
            UnitName = unitName
        });

        var existingUnit = await multi.ReadSingleOrDefaultAsync<UnitOfMeasurement>();
        var nameExists = await multi.ReadSingleAsync<bool>();

        return (existingUnit, nameExists);
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

    public async Task<bool> IsClientUsedAsync(Guid unitId, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"
        SELECT 1 FROM Balance WHERE UnitOfMeasurementId = @UnitOfMeasurementId LIMIT 1
        UNION
        SELECT 1 FROM InboundResources WHERE UnitOfMeasurementId = @UnitOfMeasurementId LIMIT 1
        UNION
        SELECT 1 FROM OutboundResource WHERE UnitOfMeasurementId = @UnitOfMeasurementId LIMIT 1
        LIMIT 1";
        
        var result = await connection.QueryFirstOrDefaultAsync<int?>(sql, new { UnitOfMeasurementId = unitId });
        return result.HasValue;
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