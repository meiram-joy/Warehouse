namespace Warehouse.Application.DTOs;

public record InboundDocumentInputDto(Guid Id,string DocumentNumber, DateTime Date, List<InboundResourceInputDto> Items);
public class InboundDocumentOutputDto
{
    public Guid Id {get; set;}
    public DateTime Date {get; set;}
    public List<InboundResourceOutputDto> Resources {get; set;}
    public string DocumentNumber {get; set;}
}
