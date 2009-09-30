using System;
using System.Collections.Generic;
using Fohjin.DDD.Domain.Events;

namespace Fohjin.DDD.Domain.Contracts
{
    public interface IEventPropagator 
    {
        void ForwardEvents(Guid aggregateId, IEnumerable<IDomainEvent> domainEvents);
    }
}