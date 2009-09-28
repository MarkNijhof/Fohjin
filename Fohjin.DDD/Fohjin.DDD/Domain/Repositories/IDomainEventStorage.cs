using System;
using System.Collections.Generic;
using Fohjin.DDD.Domain.Events;

namespace Fohjin.DDD.Domain.Repositories
{
    public interface IDomainEventStorage
    {
        IEnumerable<IDomainEvent> GetEventsAfter(int index);
        void AddEvent(Guid id, IDomainEvent domainEvent);
    }
}