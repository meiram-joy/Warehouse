using CSharpFunctionalExtensions;
using Warehouse.Domain.Currency.ValueObjects;
using Entity = Warehouse.Domain.Common.Entity;

namespace Warehouse.Domain.Currency.Entities;

public class Balance : Entity
{
    public Quantity Quantity { get; private set; }
    public Guid ResourceId { get; private set; }
    public Guid UnitOfMeasurementId { get; private set; }
    
    private Balance(Guid resourceId, Guid unitOfMeasurementId, Quantity quantity)
    {
        ResourceId = resourceId;
        UnitOfMeasurementId = unitOfMeasurementId;
        Quantity = quantity;
    }
    public static Result<Balance> Create(Guid resourceId, Guid unitOfMeasurementId, Quantity quantity)
    {
        if (resourceId == Guid.Empty)
           Result.Failure("Resource id cannot be empty");
        if (unitOfMeasurementId == Guid.Empty)
            Result.Failure("Unit of measurement id cannot be empty");
        if (quantity == null)
            Result.Failure("Quantity cannot be null");
        
        return new Balance(resourceId, unitOfMeasurementId, quantity);
    }

    public void Increase(Quantity qty) => Quantity += qty;
    public void Decrease(Quantity qty) => Quantity -= qty;
}