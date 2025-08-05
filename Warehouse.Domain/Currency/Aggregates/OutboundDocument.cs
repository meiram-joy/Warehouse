using CSharpFunctionalExtensions;
using Warehouse.Domain.Common;
using Warehouse.Domain.Currency.Entities;
using Warehouse.Domain.Currency.Enum;
using Warehouse.Domain.Currency.ValueObjects;

namespace Warehouse.Domain.Currency.Aggregates;

public class OutboundDocument : AggregateRoot
{
    public string OutboundDocumentNumber { get; private set; }
    public Guid ClientId { get; private set; }
    public DateTime Date { get; private set; }
    public ShipmentStatus Status { get; private set; }
    private readonly List<OutboundResource> _items = new();
    public IReadOnlyCollection<OutboundResource> Items => _items.AsReadOnly();
    
    private OutboundDocument(Guid id, string outboundDocumentNumber,DateTime date,ShipmentStatus status) : base(id)
    {
        if (outboundDocumentNumber == null)
            throw new ArgumentNullException(nameof(outboundDocumentNumber), "Outbound document number cannot be null.");
        if (date == null)
            throw new ArgumentNullException(nameof(date), "Date cannot be null.");
        if (status == null)
            throw new ArgumentNullException(nameof(status), "Status cannot be null.");
        
        OutboundDocumentNumber = outboundDocumentNumber;
        Date = date;
        Status = status;
    }
    
    public static Result<OutboundDocument> Create(Guid id, string outboundDocumentNumber, DateTime date, ShipmentStatus status)
    {
        return  Result.Success(new OutboundDocument(id, outboundDocumentNumber, date, status));
    }
    
    public void AddItem(Guid resourceId, Guid unitOfMeasurementId, Quantity quantity,Guid outboundResourceId)
    {
        _items.Add(new OutboundResource(outboundResourceId,resourceId, unitOfMeasurementId, quantity));
    }
    
    public void Sign()
    {
        if (!_items.Any()) throw new InvalidOperationException("Cannot sign empty document");
        Status = ShipmentStatus.Signed;
    }
    
    public void Revoke() => Status = ShipmentStatus.Revoked;
}