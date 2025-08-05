using CSharpFunctionalExtensions;
using Warehouse.Domain.Common;
using Warehouse.Domain.Currency.Entities;

namespace Warehouse.Domain.Currency.Aggregates;

public class InboundDocument : AggregateRoot
{
    public string InboundDocumentNumber { get; private set; }
    public DateTime Date { get; private set; }
    private readonly List<InboundResource> _items = new();
    public IReadOnlyCollection<InboundResource> Items => _items.AsReadOnly();
    
    private InboundDocument(Guid id,string inboundDocumentNumber, DateTime date) : base(id)
    {
        if (string.IsNullOrWhiteSpace(inboundDocumentNumber))
            throw new ArgumentException("Inbound document number cannot be null or empty", nameof(inboundDocumentNumber));
        if (date == default)
            throw new ArgumentException("Date cannot be default value", nameof(date));
        
        InboundDocumentNumber = inboundDocumentNumber;
        Date = date;
    }
    public static InboundDocument Create(Guid id, string inboundDocumentNumber, DateTime date)
    {
        return new InboundDocument(id, inboundDocumentNumber, date);
    }
    public Result AddItem(Guid resourceId, Guid unitId, Balance quantity)
    {
        _items.Add(new InboundResource(resourceId, unitId, quantity));
        return Result.Success("Item added successfully");
    }
    public Result RemoveItem(Guid itemId)
    {
        var item = _items.FirstOrDefault(x => x.ID == itemId);
        if (item != null) _items.Remove(item);
        return Result.Success("Item removed successfully");
    }
    public Result UpdateItem(Guid itemId, Balance newQty)
    {
        var item = _items.FirstOrDefault(x => x.ID == itemId)
                   ?? throw new InvalidOperationException("Item not found");
        item.UpdatenewInventoryBalance(newQty);
        return Result.Success("Item updated successfully");
    }
}