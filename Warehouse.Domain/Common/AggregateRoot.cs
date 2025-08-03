﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Domain.Common;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot(Guid id)
    {
        ID = id;
    }
    private List<IDomainEvent> _domainEvents = new();
    
    [NotMapped]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        if(_domainEvents ==  null)
            _domainEvents = new List<IDomainEvent>();
        
        if (_domainEvents.Any(s => s.GetType().Name == domainEvent.GetType().Name && s.ID == domainEvent.ID))
            return;
        
        _domainEvents.Add(domainEvent);
    }
}