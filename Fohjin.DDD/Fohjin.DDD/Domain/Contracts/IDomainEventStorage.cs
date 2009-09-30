using System;
using System.Collections.Generic;
using Fohjin.DDD.Domain.Events;

namespace Fohjin.DDD.Domain.Contracts
{
    public interface IDomainEventStorage
    {
        IEnumerable<IDomainEvent> GetAllEvents(Guid aggregateId);
        IEnumerable<IDomainEvent> GetEventsSinceLastSnapShot(Guid aggregateId);
        void SaveEvents(Guid id, IEnumerable<IDomainEvent> domainEvents);
    }
}