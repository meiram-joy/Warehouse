using Warehouse.Domain.Currency.Entities;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Infrastructure.Repositories;

public class BalanceRepository : IBalanceRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    
    public BalanceRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public Task<Balance?> GetByIdAsync(Guid id)
    {
        using var connection = _connectionFactory.CreateConnection();
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Balance>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Balance balance)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Balance balance)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}