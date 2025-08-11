using CSharpFunctionalExtensions;
using Warehouse.Domain.Currency.Enum;
using Entity = Warehouse.Domain.Common.Entity;

namespace Warehouse.Domain.Currency.Entities;

public sealed class Resource : Entity
{
    public string ResourceName { get; private set; }
    public EntityStatus Status { get; private set; }

    private Resource(string resourceName)
    {
        if (string.IsNullOrWhiteSpace(resourceName))
            throw new ArgumentException("Resource name cannot be null or empty.", nameof(resourceName));
        
        ResourceName = resourceName;
        ID = Guid.NewGuid();
    }

    public static Resource Create(string resourceName)
    {
        return new Resource(resourceName);
    }
    
    public Result Update(string resourceName)
    {
        if (string.IsNullOrWhiteSpace(resourceName))
            return Result.Failure("Имя ресурса не может быть пустым");
        ResourceName = resourceName;
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

    public Result Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            return Result.Failure("New resource name cannot be null or empty.");
        
        ResourceName = newName;
        return Result.Success();
    }
}