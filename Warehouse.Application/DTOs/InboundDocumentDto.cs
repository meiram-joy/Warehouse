namespace Warehouse.Application.DTOs;

public record InboundDocumentInputDto(string DocumentNumber, DateTime Date, List<InboundResourceInputDto> Items);
public class InboundDocumentOutputDto
{
     
    public Guid Id {get; set;}
    public DateTime Date {get; set;}
    public List<InboundResourceOutputDto> Items {get; set;}
    public string DocumentNumber {get; set;}
}
