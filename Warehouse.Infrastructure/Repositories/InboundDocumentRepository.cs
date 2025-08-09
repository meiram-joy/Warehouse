using Dapper;
using Warehouse.Domain.Currency.Aggregates;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Infrastructure.Repositories;

public class InboundDocumentRepository : IInboundDocumentRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public InboundDocumentRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        EnsureTableCreated();
    }
    
    public async Task<InboundDocument?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"SELECT * FROM InboundDocument WHERE Id = @Id";
        var document = await connection.QuerySingleOrDefaultAsync<InboundDocument>(sql, new { Id = id });
        if (document == null) return null;
        
        return document;
    }

    public async Task<InboundDocument?> GetByDocumentNumberAsync(string documentNumber, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"SELECT * FROM InboundDocument WHERE InboundDocumentNumber = @InboundDocumentNumber";
        var document = await connection.QuerySingleOrDefaultAsync<InboundDocument>(sql, new { InboundDocumentNumber = documentNumber });
        if (document == null) return null;
        
        return document;
    }

    public async Task<IReadOnlyList<InboundDocument>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"SELECT * FROM InboundDocument";
        var document = await connection.QueryAsync<InboundDocument>(sql);
        return document.Select(d => InboundDocument.Create(d.InboundDocumentNumber,d.Date).Value).ToList();
    }

    public async Task AddAsync(InboundDocument document, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"INSERT INTO InboundDocument (Id, InboundDocumentNumber, Date) 
                             VALUES (@Id, @InboundDocumentNumber, @Date)";
        await connection.ExecuteAsync(sql, new
        {
            Id = document.ID,
            InboundDocumentNumber = document.InboundDocumentNumber,
            Date = document.Date
        });
    }

    public async Task UpdateAsync(InboundDocument document, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"UPDATE InboundDocument 
                             SET InboundDocumentNumber = @InboundDocumentNumber, Date = @Date
                             WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new
        {
            Id = document.ID,
            InboundDocumentNumber = document.InboundDocumentNumber,
            Date = document.Date
        });
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"DELETE FROM InboundDocument WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }
    private void EnsureTableCreated()
    {
        using var connection = _connectionFactory.CreateConnection(); //Todo Исправить ID должны быть Text
        connection.Open();
        var createTableSql = @"
            CREATE TABLE IF NOT EXISTS InboundDocument (
            Id TEXT PRIMARY KEY,
            InboundDocumentNumber TEXT NOT NULL UNIQUE,
            Date DATETIME NOT NULL
            );
        ";
        using var cmd = connection.CreateCommand();
        cmd.CommandText = createTableSql;
        cmd.ExecuteNonQuery();
    }
}