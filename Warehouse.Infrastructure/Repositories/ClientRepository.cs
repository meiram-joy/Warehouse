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
        EnsureTableCreated();
    }

    public async Task<Client?> GetByNameAndAddressAsync(string clientName,string address,CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"SELECT * FROM Client WHERE ClientName = @ClientName AND Address = @Address";
        var client = await connection.QuerySingleOrDefaultAsync<Client>(sql, new { ClientName = clientName, Address = address });
        if (client == null) return null;
        
        return client;
    }

    public async Task<bool> CheckForCreateAsync(string name, string address, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
        SELECT CASE WHEN EXISTS (
            SELECT 1 
            FROM Client 
            WHERE ClientName = @ClientName 
              AND Address = @Address
        ) 
        THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END;
    ";

        var nameExists = await connection.ExecuteScalarAsync<bool>(sql, new
        {
            ClientName = name,
            Address = address
        });

        return nameExists;
    }

    public async Task<(Client? clientToCreate, bool nameExists)> CheckForUpdateAsync(string name, string address, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        var sql = @"
        SELECT * 
        FROM Client 
        WHERE ClientName = @ClientName AND Address = @Address;
        SELECT CASE WHEN EXISTS (
            SELECT 1 
            FROM Client 
            WHERE ClientName = @ClientName
        ) 
        THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END;
    ";

        using var multi = await connection.QueryMultipleAsync(sql, new
        {
            ClientName = name,
            Address = address
        });

        var existingClient = await multi.ReadSingleOrDefaultAsync<Client>();
        var nameExists = await multi.ReadSingleAsync<bool>();

        return (existingClient, nameExists);
    }

    public async Task<Client?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"SELECT * FROM Client WHERE Id = @Id";
        var client = await connection.QuerySingleOrDefaultAsync<Client>(sql, new { Id });
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

    public async Task<bool> IsClientUsedAsync(Guid clientId,CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"
        SELECT 1
        FROM ReceiptDocuments
        WHERE ClientId = @ClientId
        LIMIT 1";
        
        var result = await connection.QueryFirstOrDefaultAsync<int?>(sql, new { ClientId = clientId });
        return result.HasValue;
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
                Status INTEGER NULL
            );
        ";
        using var cmd = connection.CreateCommand();
        cmd.CommandText = createTableSql;
        cmd.ExecuteNonQuery();
    }
}