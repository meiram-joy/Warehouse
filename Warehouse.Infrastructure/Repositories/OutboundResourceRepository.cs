using Warehouse.Domain.Currency.Entities;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Infrastructure.Repositories;

public class OutboundResourceRepository : IOutboundResourceRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    
    public OutboundResourceRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public Task<OutboundResource?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OutboundResource>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(OutboundResource resource)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(OutboundResource resource)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}