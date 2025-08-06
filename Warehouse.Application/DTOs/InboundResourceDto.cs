namespace Warehouse.Application.DTOs;

public record InboundResourceInputDto(Guid ResourceId, Guid UnitOfMeasurementId,decimal Quantity);
public record InboundResourceOutputDto( Guid Id,Guid ResourceId, Guid UnitOfMeasurementId,decimal Quantity);