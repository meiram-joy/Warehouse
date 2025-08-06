using CSharpFunctionalExtensions;
using Warehouse.Domain.Currency.Enum;
using Entity = Warehouse.Domain.Common.Entity;

namespace Warehouse.Domain.Currency.Entities;

public sealed class UnitOfMeasurement : Entity
{
    public string UnitName { get; private set; }
    public EntityStatus Status { get; private set; }
    
    private UnitOfMeasurement(Guid id,string unitName, EntityStatus status)
    {
        UnitName = unitName;
        Status = status;
        ID = id;
    }
    public static UnitOfMeasurement Create(Guid id,string unitName, EntityStatus status)
    {
        if (string.IsNullOrWhiteSpace(unitName))
            throw new ArgumentException("Resource name cannot be null or empty.", nameof(unitName));
        if (id == Guid.Empty)
            throw new ArgumentException("ID cannot be an empty Guid.", nameof(id));
        
        return new UnitOfMeasurement(id,unitName, status);
    }
    
    public Result Archive()
    {
        Status = EntityStatus.Archived;
        return Result.Success();
    }
}