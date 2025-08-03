using MediatR;

namespace Warehouse.Domain.Common;

public interface IDomainEvent : INotification
{
    Guid ID {get; }
}