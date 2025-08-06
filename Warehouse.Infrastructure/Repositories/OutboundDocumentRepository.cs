using Dapper;
using Warehouse.Domain.Currency.Aggregates;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Infrastructure.Repositories;

public class OutboundDocumentRepository : IOutboundDocumentRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    
    public OutboundDocumentRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        EnsureTableCreated();
    }

    public async Task<OutboundDocument?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"SELECT * FROM OutboundDocument WHERE Id = @Id";
        var outboundDocument = await connection.QuerySingleOrDefaultAsync<OutboundDocument>(sql, new { Id = id });
        if (outboundDocument == null) return null;
        
        return outboundDocument;
    }

    public async Task<IEnumerable<OutboundDocument>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"SELECT * FROM OutboundDocument";
        var outboundDocument = await connection.QueryAsync<OutboundDocument>(sql);
        return outboundDocument;
    }

    public async Task AddAsync(OutboundDocument document, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"INSERT INTO OutboundDocument (Id, ClientId, Date, ShipmentStatus) 
                             VALUES (@Id, @ClientId, @Date, @ShipmentStatus)";
        await connection.ExecuteAsync(sql, new
        {
            Id = document.ID,
            ClientId = document.ClientId,
            Date = document.Date,
            ShipmentStatus = document.Status
        });
    }

    public async Task UpdateAsync(OutboundDocument document, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"UPDATE OutboundDocument 
                             SET ClientId = @ClientId, Date = @Date, ShipmentStatus = @ShipmentStatus 
                             WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new
        {
            Id = document.ID,
            ClientId = document.ClientId,
            Date = document.Date,
            ShipmentStatus = document.Status
        });
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"DELETE FROM OutboundDocument WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }
    private void EnsureTableCreated()
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();
        var createTableSql = @"
            CREATE TABLE IF NOT EXISTS OutboundDocument (
            Id TEXT PRIMARY KEY,
            ClientId TEXT NOT NULL,
            Date DATETIME NOT NULL,
            ShipmentStatus INTEGER NOT NULL,
            FOREIGN KEY (ClientId) REFERENCES Client(Id),
            );
        ";
        using var cmd = connection.CreateCommand();
        cmd.CommandText = createTableSql;
        cmd.ExecuteNonQuery();
    }
    
}