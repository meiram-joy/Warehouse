using Warehouse.Domain.Currency.Enum;

namespace Warehouse.Application.DTOs;

public record ClientInputDto(string Name,string Address);
public record ClientOutputDto(Guid Id,string Name,string Address,EntityStatus Status);