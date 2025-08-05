

using CSharpFunctionalExtensions;
using Entity = Warehouse.Domain.Common.Entity;

namespace Warehouse.Domain.Currency.Entities;

public class InboundResource : Entity
{
    public Guid ResourceId { get; private set; }
    public Guid UnitOfMeasurementId { get; private set; }
    public Balance Balance { get; private set; }
    
    //Todo : Кажется я перепутал местами InboundResource c InboundDocument 

    internal InboundResource(Guid resourceId, Guid unitOfMeasurementId, Balance balance)
    {
        ResourceId = resourceId;
        UnitOfMeasurementId = unitOfMeasurementId;
        Balance = balance;
    }
    public static Result<InboundResource> Create(Guid resourceId, Guid unitOfMeasurementId, Balance balance)
    {
        if (resourceId == Guid.Empty)
            return Result.Failure<InboundResource>("Resource ID cannot be empty.");

        if (unitOfMeasurementId == Guid.Empty)
            return Result.Failure<InboundResource>("Unit of Measurement ID cannot be empty.");

        if (balance == null)
            return Result.Failure<InboundResource>("Inventory Balance cannot be null.");

        return new InboundResource(resourceId, unitOfMeasurementId, balance);
    }
    public void UpdatenewInventoryBalance(Balance newinventoryBalance) => Balance = newinventoryBalance;
}