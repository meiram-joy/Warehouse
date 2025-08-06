namespace Warehouse.Application.DTOs;

public record BalanceInputDto(Guid ResourceId,Guid UnitOfMeasurementId,decimal Quantity);
public record BalanceOutputDto(Guid Id,Guid ResourceId,Guid UnitOfMeasurementId,decimal Quantity);

