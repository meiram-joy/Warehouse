using Warehouse.Domain.Currency.Enum;

namespace Warehouse.Application.DTOs;

public record OutboundDocumentInputDto(string DocumentNumber,Guid ClientId, DateTime Date,List<OutboundResourceInputDto> Items);
public record OutboundDocumentOutputDto(Guid Id,string DocumentNumber, Guid ClientId,DateTime Date,ShipmentStatus Status,List<OutboundResourceOutputDto> Items);