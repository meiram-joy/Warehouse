using Warehouse.Domain.Currency.ValueObjects;

namespace Warehouse.Application.DTOs;

public record InboundResourceInputDto(Guid ResourceId, Guid UnitOfMeasurementId,Quantity Quantity);
public class InboundResourceOutputDto
{
    public Guid Id {get; set;}
    public Guid DocumentId {get; set;} 
    public Guid UnitOfMeasurementId {get; set;}
    public Quantity Quantity {get; set;}
}