using CSharpFunctionalExtensions;
using Warehouse.Domain.Currency.Enum;
using Entity = Warehouse.Domain.Common.Entity;

namespace Warehouse.Domain.Currency.Entities;

public sealed class UnitOfMeasurement : Entity
{
    public string UnitName { get; private set; }
    public EntityStatus Status { get; private set; }
    
    private UnitOfMeasurement(string unitName)
    {
        if (string.IsNullOrWhiteSpace(unitName))
            throw new ArgumentException("Resource name cannot be null or empty.", nameof(unitName));
        
        UnitName = unitName;
        ID = Guid.NewGuid();
    }
    public static UnitOfMeasurement Create(string unitName)
    {
        return new UnitOfMeasurement(unitName);
    }
    
    public static UnitOfMeasurement Update(string unitName)
    {
        return new UnitOfMeasurement(unitName);
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