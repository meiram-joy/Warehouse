using Dapper;
using Warehouse.Domain.Currency.Entities;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    
    public ClientRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"SELECT * FROM Client WHERE Id = @Id";
        var client = await connection.QuerySingleOrDefaultAsync<Client>(sql, new { Id = id });
        if (client == null) return null;
        
        return client;
    }

    public async Task<IEnumerable<Client>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"SELECT * FROM Client";
        var client = await connection.QueryAsync<Client>(sql);
        return client;
    }

    public async Task AddAsync(Client client, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"INSERT INTO Client (Id, ClientName, Address, Status) 
                             VALUES (@Id, @ClientName, @Address, @Status)";
        await connection.ExecuteAsync(sql, new
        {
            Id = client.ID,
            ClientName = client.ClientName,
            Address = client.Address,
            Status = client.Status
        });
    }

    public async Task UpdateAsync(Client client, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"UPDATE Client 
                             SET ClientName = @ClientName, Address = @Address, Status = @Status 
                             WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new
        {
            Id = client.ID,
            ClientName = client.ClientName,
            Address = client.Address,
            Status = client.Status
        });
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"DELETE FROM Client WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }
    private void EnsureTableCreated()
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();
        var createTableSql = @"
            CREATE TABLE IF NOT EXISTS Client (
                Id TEXT PRIMARY KEY,
                ClientName VARCHAR(50) NOT NULL,
                Address VARCHAR(100) NOT NULL,
                Status INTEGER NOT NULL
            );
        ";
        using var cmd = connection.CreateCommand();
        cmd.CommandText = createTableSql;
        cmd.ExecuteNonQuery();
    }
}