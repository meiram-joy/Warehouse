using Dapper;
using Warehouse.Domain.Currency.Entities;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Infrastructure.Repositories;

public class InboundResourceRepository : IInboundResourceRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public InboundResourceRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        EnsureTableCreated();
    }

    public async Task<InboundResource?> GetByInboundDocumentIdAsync(Guid inboundDocumentId, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"SELECT * FROM InboundResource WHERE InboundDocumentId = @InboundDocumentId";
        var inboundResource = await connection.QuerySingleOrDefaultAsync<InboundResource>(sql, new { InboundDocumentId = inboundDocumentId });
        if (inboundResource == null) return null;
        
        return inboundResource;
    }

    public async Task<IReadOnlyList<InboundResource>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"SELECT * FROM InboundResource";
        var inboundResource = await connection.QueryAsync<InboundResource>(sql);
        return inboundResource.Select(r => InboundResource.Create(r.ID, r.UnitOfMeasurementId, r.Quantity,r.DocumentId).Value).ToList();
    }

    public async Task AddAsync(Guid inboundDocumentId,InboundResource resource, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"INSERT INTO InboundResource (Id,InboundDocumentId, ResourceId, UnitOfMeasurementId, Quantity) 
                             VALUES (@Id, @InboundDocumentId, @ResourceId, @UnitOfMeasurementId, @Quantity)";
        await connection.ExecuteAsync(sql, new
        {
            Id = resource.ID,
            InboundDocumentId = inboundDocumentId,
            ResourceId = resource.ID,
            UnitOfMeasurementId = resource.UnitOfMeasurementId,
            Quantity = resource.Quantity
        });
    }

    public async Task UpdateAsync(Guid inboundDocumentId,InboundResource resource, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"UPDATE InboundResource 
                             SET ResourceId = @ResourceId, InboundDocumentId = @InboundDocumentId ,UnitOfMeasurementId = @UnitOfMeasurementId, Quantity = @Quantity 
                             WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new
        {
            Id = resource.ID,
            InboundDocumentId = inboundDocumentId,
            ResourceId = resource.ID,
            UnitOfMeasurementId = resource.UnitOfMeasurementId,
            Quantity = resource.Quantity
        });
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"DELETE FROM InboundResource WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }
    private void EnsureTableCreated()
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();
        var createTableSql = @"
            CREATE TABLE IF NOT EXISTS InboundResources (
            Id TEXT PRIMARY KEY,
            InboundDocumentId TEXT NOT NULL,
            ResourceId TEXT NOT NULL,
            UnitOfMeasurementId TEXT NOT NULL,
            Quantity DECIMAL(18,4) NOT NULL,
            FOREIGN KEY (InboundDocumentId) REFERENCES InboundDocument(Id),
            FOREIGN KEY (ResourceId) REFERENCES Resource(Id),
            FOREIGN KEY (UnitOfMeasurementId) REFERENCES UnitOfMeasurement(Id)
        );
        ";
        using var cmd = connection.CreateCommand();
        cmd.CommandText = createTableSql;
        cmd.ExecuteNonQuery();
    }
}