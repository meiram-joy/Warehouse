using CSharpFunctionalExtensions;
using Warehouse.Domain.Currency.Enum;
using Entity = Warehouse.Domain.Common.Entity;

namespace Warehouse.Domain.Currency.Entities;

public sealed class Resource : Entity
{
    public string ResourceName { get; private set; }
    public EntityStatus Status { get; private set; }
    
    private Resource(Guid id,string resourceName, EntityStatus status)
    {
        ResourceName = resourceName;
        Status = status;
        ID = id;
    }
    public static Resource Create(Guid id,string resourceName, EntityStatus status)
    {
        if (string.IsNullOrWhiteSpace(resourceName))
            throw new ArgumentException("Resource name cannot be null or empty.", nameof(resourceName));
        if (id == Guid.Empty)
            throw new ArgumentException("ID cannot be an empty Guid.", nameof(id));
        
        return new Resource(id,resourceName, status);
    }

    public Result Archive()
    {
        Status = EntityStatus.Archived;
        return Result.Success();
    }

    public Result Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            return Result.Failure("New resource name cannot be null or empty.");
        
        ResourceName = newName;
        return Result.Success();
    }
}