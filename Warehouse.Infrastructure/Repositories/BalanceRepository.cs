using Dapper;
using Warehouse.Domain.Currency.Entities;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Infrastructure.Repositories;

public class BalanceRepository : IBalanceRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    
    public BalanceRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        EnsureTableCreated();
    }
    
    public async Task<Balance?> GetByResourceIdAndUnitIdAsync(Guid resourceId,Guid unitId,CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"SELECT * FROM Balances WHERE ResourceId = @ResourceId AND UnitOfMeasurementId = @UnitId";
        var balance = await connection.QuerySingleOrDefaultAsync<Balance>(sql, new { ResourceId = resourceId, UnitOfMeasurementId = unitId });
        if (balance == null) return null;
        
        return balance;
    }

    public async Task<IReadOnlyList<Balance>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"SELECT * FROM Balances";
        var balances = await connection.QueryAsync<Balance>(sql);
        return balances
            .Select(r =>
                Balance.Create(
                    r.ResourceId,
                    r.UnitOfMeasurementId,
                    r.Quantity
                ).Value
            ).ToList();
    }

    public async Task AddAsync(Balance balance, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"INSERT INTO Balances (Id, ResourceId, UnitOfMeasurementId, Quantity) 
                             VALUES (@Id, @ResourceId, @UnitOfMeasurementId, @Quantity)";
        await connection.ExecuteAsync(sql, new
        {
            Id = balance.ID,
            ResourceId = balance.ResourceId,
            UnitOfMeasurementId = balance.UnitOfMeasurementId,
            Quantity = balance.Quantity
        });
    }

    public async Task UpdateAsync(Balance balance, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"UPDATE Balances 
                             SET ResourceId = @ResourceId, UnitOfMeasurementId = @UnitOfMeasurementId, Quantity = @Quantity 
                             WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new
        {
            Id = balance.ID,
            ResourceId = balance.ResourceId,
            UnitOfMeasurementId = balance.UnitOfMeasurementId,
            Quantity = balance.Quantity
        });
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"DELETE FROM Balances WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }
    private void EnsureTableCreated()
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();
        var createTableSql = @"
            CREATE TABLE IF NOT EXISTS Balances (
            Id TEXT PRIMARY KEY,
            Quantity DECIMAL(18, 4) NOT NULL,
            ResourceId TEXT NOT NULL,
            UnitOfMeasurementId TEXT NOT NULL,
            FOREIGN KEY (ResourceId) REFERENCES Resource(Id),
            FOREIGN KEY (UnitOfMeasurementId) REFERENCES UnitOfMeasurement(Id)
            );
        ";
        using var cmd = connection.CreateCommand();
        cmd.CommandText = createTableSql;
        cmd.ExecuteNonQuery();
    }
}