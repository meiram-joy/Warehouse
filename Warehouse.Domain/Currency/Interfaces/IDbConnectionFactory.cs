using System.Data;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}