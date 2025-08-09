using CSharpFunctionalExtensions;
using Warehouse.Domain.Common;
using Warehouse.Domain.Currency.Entities;
using Warehouse.Domain.Currency.ValueObjects;

namespace Warehouse.Domain.Currency.Aggregates;

public class InboundDocument : AggregateRoot
{
    public string InboundDocumentNumber { get; private set; }
    public DateTime Date { get; private set; }
    private readonly List<InboundResource> _items = new();
    public IReadOnlyCollection<InboundResource> Items => _items.AsReadOnly();
    
    private InboundDocument(string inboundDocumentNumber, DateTime date) : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(inboundDocumentNumber))
            throw new ArgumentException("Inbound document number cannot be null or empty", nameof(inboundDocumentNumber));
        if (date == default)
            throw new ArgumentException("Date cannot be default value", nameof(date));
        
        InboundDocumentNumber = inboundDocumentNumber;
        Date = date;
    }
    public static Result<InboundDocument> Create(string inboundDocumentNumber, DateTime date)
    {
        return  Result.Success(new InboundDocument(inboundDocumentNumber, date));
    }
    public  Result  AddItem(Guid resourceId, Guid unitId, Quantity quantity, Guid documentId)
    {
        _items.Add(new InboundResource(resourceId, unitId, quantity, documentId));
        return Result.Success("Item added successfully");
    }
    public Result RemoveItem(Guid resourceId)
    {
        var item = _items.FirstOrDefault(x => x.ID == resourceId);
        if (item != null) _items.Remove(item);
        return Result.Success("Item removed successfully");
    }
    public Result UpdateItem(Guid itemId, Balance newQty)
    {
        // var item = _items.FirstOrDefault(x => x.ID == itemId)
        //            ?? throw new InvalidOperationException("Item not found");
        // item.UpdatenewInventoryBalance(newQty);
        // return Result.Success("Item updated successfully");
        return Result.Success("Item updated successfully");
    }
}