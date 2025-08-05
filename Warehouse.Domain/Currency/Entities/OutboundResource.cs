using Warehouse.Domain.Common;
using Warehouse.Domain.Currency.ValueObjects;

namespace Warehouse.Domain.Currency.Entities;

public class OutboundResource : Entity
{
    public Guid ResourceId { get; private set; }
    public Guid UnitId { get; private set; }
    public Quantity Quantity { get; private set; }

    internal OutboundResource(Guid id,Guid resourceId, Guid unitOfMeasurementId, Quantity quantity)
    {
        ID = id;
        ResourceId = resourceId;
        UnitId = unitOfMeasurementId;
        Quantity = quantity;
    }
}