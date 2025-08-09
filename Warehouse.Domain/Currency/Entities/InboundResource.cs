using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using Warehouse.Domain.Currency.ValueObjects;
using Entity = Warehouse.Domain.Common.Entity;

namespace Warehouse.Domain.Currency.Entities;

public sealed class InboundResource : Entity
{
    public Guid DocumentId { get; set; }
    public Guid UnitOfMeasurementId { get; private set; }
    public Quantity Quantity { get; private set; }
    

    internal InboundResource(Guid resourceId, Guid unitOfMeasurementId, Quantity quantity, Guid documentId)
    {
        if (resourceId == Guid.Empty)
            throw new ArgumentException("Resource ID cannot be empty.");

        if (unitOfMeasurementId == Guid.Empty)
            throw new ArgumentException("Unit of Measurement ID cannot be empty.");

        if (quantity == null)
            throw new ArgumentException("Inventory Balance cannot be null.");
        
        ID = resourceId;
        UnitOfMeasurementId = unitOfMeasurementId;
        Quantity = quantity;
        DocumentId = documentId;
    }

    public static Result<InboundResource> Create(Guid resourceId, Guid unitOfMeasurementId, Quantity quantity,Guid documentId)
    {
        return Result.Success(new InboundResource(resourceId, unitOfMeasurementId, quantity, documentId));
    }
}