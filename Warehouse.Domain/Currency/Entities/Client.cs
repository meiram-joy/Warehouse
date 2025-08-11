using CSharpFunctionalExtensions;
using Warehouse.Domain.Currency.Enum;
using Entity = Warehouse.Domain.Common.Entity;

namespace Warehouse.Domain.Currency.Entities;

public sealed class Client : Entity
{
    public string ClientName { get; private set; }
    public string Address { get; private set; }
    public EntityStatus Status { get; private set; }
    
    private Client(string clientName,string address)
    {
        if (string.IsNullOrWhiteSpace(clientName))
            throw new ArgumentException("Название клиента не может быть пустым", nameof(clientName));
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Адрес клиента не может быть пустым", nameof(address));
        
        ID = Guid.NewGuid();
        ClientName = clientName;
        Address = address;
    }
    public static Client Create(string clientName,string address)
    {
        return new Client(clientName, address);
    }
    
    public Result Update(string clientName,string address)
    {
        if (string.IsNullOrWhiteSpace(clientName))
           return Result.Failure("Название клиента не может быть пустым");
        if (string.IsNullOrWhiteSpace(address))
            return Result.Failure("Адрес клиента не может быть пустым");
        ClientName = clientName;
        Address = address;
        return Result.Success();
    }
    public Result Archive()
    {
        Status = EntityStatus.Archived;
        return Result.Success();
    }
    public Result Active()
    {
        Status = EntityStatus.Active;
        return Result.Success();
    }
}