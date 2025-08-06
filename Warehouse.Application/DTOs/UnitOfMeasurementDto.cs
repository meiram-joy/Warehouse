using Warehouse.Domain.Currency.Enum;

namespace Warehouse.Application.DTOs;

public record class UnitOfMeasurementInputDto(string Name);
public record class UnitOfMeasurementOutputDto( Guid Id,string Name, EntityStatus Status);

