

using CSharpFunctionalExtensions;
using Entity = Warehouse.Domain.Common.Entity;

namespace Warehouse.Domain.Currency.Entities;

public sealed class InboundResource : Entity
{
    public Guid ResourceId { get; private set; }
    public Guid UnitOfMeasurementId { get; private set; }
    public Balance Balance { get; private set; }
    
    //Todo : Кажется я перепутал местами InboundResource c InboundDocument 

    internal InboundResource(Guid resourceId, Guid unitOfMeasurementId, Balance balance)
    {
        if (resourceId == Guid.Empty)
            throw new ArgumentException("Resource ID cannot be empty.");

        if (unitOfMeasurementId == Guid.Empty)
            throw new ArgumentException("Unit of Measurement ID cannot be empty.");

        if (balance == null)
            throw new ArgumentException("Inventory Balance cannot be null.");
        
        ResourceId = resourceId;
        UnitOfMeasurementId = unitOfMeasurementId;
        Balance = balance;
    }
    public static Result<InboundResource> Create(Guid resourceId, Guid unitOfMeasurementId, Balance balance)
    {
        return Result.Success(new InboundResource(resourceId, unitOfMeasurementId, balance));
    }
    public void UpdatenewInventoryBalance(Balance newinventoryBalance) => Balance = newinventoryBalance;
}