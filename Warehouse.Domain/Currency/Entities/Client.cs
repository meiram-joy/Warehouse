using CSharpFunctionalExtensions;
using Warehouse.Domain.Currency.Enum;
using Entity = Warehouse.Domain.Common.Entity;

namespace Warehouse.Domain.Currency.Entities;

public sealed class Client : Entity
{
    public string ClientName { get; private set; }
    public string Address { get; private set; }
    public EntityStatus Status { get; private set; }
    
    private Client(Guid id,string clientName, EntityStatus status,string address)
    {
        if (string.IsNullOrWhiteSpace(clientName))
            throw new ArgumentException("Resource name cannot be null or empty.", nameof(clientName));
        if (id == Guid.Empty)
            throw new ArgumentException("ID cannot be an empty Guid.", nameof(id));
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address cannot be null or empty.", nameof(address));
        
        ID = id;
        ClientName = clientName;
        Address = address;
        Status = status;
    }
    public static Result<Client> Create(Guid id,string clientName, EntityStatus status,string address)
    {
        return Result.Success(new Client(id, clientName, status, address));
    }
    public Result Archive()
    {
        Status = EntityStatus.Archived;
        return Result.Success();
    }
}