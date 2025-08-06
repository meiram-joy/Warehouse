using Warehouse.Domain.Currency.ValueObjects;

namespace Warehouse.Application.DTOs;

public record BalanceInputDto(Guid ResourceId,Guid UnitOfMeasurementId,Quantity Quantity);
public record BalanceOutputDto(Guid Id,Guid ResourceId,Guid UnitOfMeasurementId,Quantity Quantity);

