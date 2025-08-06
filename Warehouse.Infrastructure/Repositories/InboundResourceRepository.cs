using Warehouse.Domain.Currency.Entities;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Infrastructure.Repositories;

public class InboundResourceRepository : IInboundResourceRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public InboundResourceRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public Task<InboundResource?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<InboundResource>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(InboundResource resource)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(InboundResource resource)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}