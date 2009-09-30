using System;
using System.Collections.Generic;
using Fohjin.DDD.Domain.Contracts;
using Fohjin.DDD.Domain.Events;

namespace Fohjin.DDD.Infrastructure
{
    public class EventPropagator : IEventPropagator
    {
        public void ForwardEvents(Guid aggregateId, IEnumerable<IDomainEvent> domainEvents)
        {
        }
    }
}