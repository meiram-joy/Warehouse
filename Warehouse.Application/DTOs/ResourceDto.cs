using Warehouse.Domain.Currency.Enum;

namespace Warehouse.Application.DTOs;

public record class ResourceInputDto(string Name);
public record class ResourceOutputDto(Guid Id, string Name, EntityStatus Status);

