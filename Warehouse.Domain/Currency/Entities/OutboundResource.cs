using CSharpFunctionalExtensions;
using Warehouse.Domain.Currency.ValueObjects;
using Entity = Warehouse.Domain.Common.Entity;

namespace Warehouse.Domain.Currency.Entities;

public sealed class OutboundResource : Entity
{
    public Guid ResourceId { get; private set; }
    public Guid UnitId { get; private set; }
    public Quantity Quantity { get; private set; }

    internal OutboundResource(Guid id,Guid resourceId, Guid unitOfMeasurementId, Quantity quantity)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("ID cannot be empty.", nameof(id));
        if (resourceId == Guid.Empty)
            throw new ArgumentException("Resource ID cannot be empty.", nameof(resourceId));
        if (unitOfMeasurementId == Guid.Empty)
            throw new ArgumentException("Unit of Measurement ID cannot be empty.", nameof(unitOfMeasurementId));
        if (quantity == null)
            throw new ArgumentNullException(nameof(quantity), "Quantity cannot be null.");
        if (quantity.Value <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
        
        ID = id;
        ResourceId = resourceId;
        UnitId = unitOfMeasurementId;
        Quantity = quantity;
    }

    public static Result<OutboundResource> Create(Guid id, Guid resourceId, Guid unitId, Quantity quantity)
    {
        return Result.Success(new OutboundResource(id, resourceId, unitId, quantity));
    }
}