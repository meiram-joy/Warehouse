using Dapper;
using Warehouse.Domain.Currency.Entities;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Infrastructure.Repositories;

public class ResourceRepository : IResourceRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    
    public ResourceRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        EnsureTableCreated();
    }

    public async Task<Resource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"SELECT * FROM Resource WHERE Id = @Id";
        var resource = await connection.QuerySingleOrDefaultAsync<Resource>(sql, new { Id = id });
        if (resource == null) return null;
        
        return resource;
    }

    public async Task<Resource?> GetByNameAsync(string resourceName, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"SELECT * FROM Resource WHERE ResourceName = @ResourceName";
        var resource = await connection.QuerySingleOrDefaultAsync<Resource>(sql, new { ResourceName = resourceName });
        if (resource == null) return null;
        
        return resource;
    }

    public async Task<IEnumerable<Resource>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"SELECT * FROM Resource";
        var resource = await connection.QueryAsync<Resource>(sql);
        return resource;
    }

    public async Task AddAsync(Resource resource, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"INSERT INTO Resource (Id, ResourceName, Status) 
                             VALUES (@Id, @ResourceName, @Status)";
        await connection.ExecuteAsync(sql, new
        {
            Id = resource.ID,
            ResourceName = resource.ResourceName,
            Status = resource.Status
        });
    }

    public async Task UpdateAsync(Resource resource, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"UPDATE Resource 
                             SET ResourceName = @ResourceName, Status = @Status
                             WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new
        {
            Id = resource.ID,
            ResourceName = resource.ResourceName,
            Status = resource.Status
        });
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"DELETE FROM Resource WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }

    public async Task<bool> IsClientUsedAsync(Guid resourceId, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"
        SELECT 1 FROM Balance WHERE ResourceId = @ResourceId LIMIT 1
        UNION
        SELECT 1 FROM InboundResources WHERE ResourceId = @ResourceId LIMIT 1
        UNION
        SELECT 1 FROM OutboundResource WHERE ResourceId = @ResourceId LIMIT 1
        LIMIT 1";
        var result = await connection.QueryFirstOrDefaultAsync<int?>(sql, new { ResourceId = resourceId });
        return result.HasValue;
    }

    private void EnsureTableCreated()
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();
        var createTableSql = @"
            CREATE TABLE IF NOT EXISTS Resource (
            Id TEXT PRIMARY KEY,
            ResourceName TEXT NOT NULL,
            Status INTEGER NOT NULL
            );
        ";
        using var cmd = connection.CreateCommand();
        cmd.CommandText = createTableSql;
        cmd.ExecuteNonQuery();
    }
}