namespace Warehouse.Application.DTOs;

public record OutboundResourceInputDto(Guid ResourceId,Guid UnitOfMeasurementId,decimal Quantity);
public record OutboundResourceOutputDto(Guid Id,Guid ResourceId,Guid UnitOfMeasurementId,decimal Quantity);