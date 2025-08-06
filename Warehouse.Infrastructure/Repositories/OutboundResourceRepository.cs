using Dapper;
using Warehouse.Domain.Currency.Entities;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Infrastructure.Repositories;

public class OutboundResourceRepository : IOutboundResourceRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    
    public OutboundResourceRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        EnsureTableCreated();
    }

    public async Task<OutboundResource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"SELECT * FROM OutboundResource WHERE Id = @Id";
        var outboundResource = await connection.QuerySingleOrDefaultAsync<OutboundResource>(sql, new { Id = id });
        if (outboundResource == null) return null;
        
        return outboundResource; 
    }

    public async Task<IEnumerable<OutboundResource>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"SELECT * FROM OutboundResource";
        var outboundResource = await connection.QueryAsync<OutboundResource>(sql);
        return outboundResource;
    }

    public async Task AddAsync(OutboundResource resource, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"INSERT INTO OutboundResource (Id, ResourceId, UnitOfMeasurementId, Quantity) 
                             VALUES (@Id, @ResourceId, @UnitOfMeasurementId, @Quantity)";
        await connection.ExecuteAsync(sql, new
        {
            Id = resource.ID,
            ResourceId = resource.ResourceId,
            UnitOfMeasurementId = resource.UnitOfMeasurementId,
            Quantity = resource.Quantity
        });
    }

    public async Task UpdateAsync(OutboundResource resource, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"UPDATE OutboundResource 
                             SET ResourceId = @ResourceId, UnitOfMeasurementId = @UnitOfMeasurementId, Quantity = @Quantity 
                             WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new
        {
            Id = resource.ID,
            ResourceId = resource.ResourceId,
            UnitOfMeasurementId = resource.UnitOfMeasurementId,
            Quantity = resource.Quantity
        });
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"DELETE FROM OutboundResource WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }
    private void EnsureTableCreated()
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();
        var createTableSql = @"
            CREATE TABLE IF NOT EXISTS OutboundResource (
            Id TEXT PRIMARY KEY,
            ResourceId TEXT NOT NULL,
            UnitOfMeasurementId TEXT NOT NULL,
            Quantity DECIMAL(18, 4) NOT NULL,
            FOREIGN KEY (ResourceId) REFERENCES Resource(Id),
            FOREIGN KEY (UnitOfMeasurementId) REFERENCES UnitOfMeasurement(Id)
            );
        ";
        using var cmd = connection.CreateCommand();
        cmd.CommandText = createTableSql;
        cmd.ExecuteNonQuery();
    }
}