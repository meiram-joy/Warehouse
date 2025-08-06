using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Infrastructure.Repositories;

public class SqliteDbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public SqliteDbConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }
    
    public SqliteConnection CreateConnection()
    {
        return new SqliteConnection(_connectionString);
    }
}