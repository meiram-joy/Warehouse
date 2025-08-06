using Microsoft.Data.Sqlite;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IDbConnectionFactory
{
    SqliteConnection CreateConnection();
}