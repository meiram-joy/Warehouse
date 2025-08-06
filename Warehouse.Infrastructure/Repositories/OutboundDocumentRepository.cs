using Warehouse.Domain.Currency.Aggregates;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Infrastructure.Repositories;

public class OutboundDocumentRepository : IOutboundDocumentRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    
    public OutboundDocumentRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public Task<OutboundDocument?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OutboundDocument>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(OutboundDocument document)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(OutboundDocument document)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}