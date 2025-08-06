namespace Warehouse.Application.DTOs;

public record InboundDocumentDto(Guid ResourceId, Guid UnitId, decimal Quantity);