using Warehouse.Domain.Currency.Aggregates;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Infrastructure.Repositories;

public class InboundDocumentRepository : IInboundDocumentRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public InboundDocumentRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public Task<InboundDocument?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<InboundDocument>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(InboundDocument document)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(InboundDocument document)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}