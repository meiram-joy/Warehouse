namespace Warehouse.Application.DTOs;

public record InboundDocumentInputDto(string DocumentNumber, DateTime Date, List<InboundResourceInputDto> Items);
public record InboundDocumentOutputDto(Guid Id, string DocumentNumber, DateTime Date, List<InboundResourceOutputDto> Items);