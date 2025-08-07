using Warehouse.Domain.Currency.ValueObjects;

namespace Warehouse.Application.DTOs;

public record InboundResourceInputDto(Guid ResourceId, Guid UnitOfMeasurementId,Quantity Quantity);
public record InboundResourceOutputDto( Guid Id,Guid ResourceId, Guid UnitOfMeasurementId,Quantity Quantity);