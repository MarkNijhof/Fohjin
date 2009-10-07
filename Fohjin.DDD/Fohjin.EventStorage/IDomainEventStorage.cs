using System;
using System.Collections.Generic;
using Fohjin.DDD.Domain;
using Fohjin.DDD.Events;

namespace Fohjin.EventStorage
{
    public interface IDomainEventStorage
    {
        IEnumerable<IDomainEvent> GetAllEvents(Guid eventProviderId);
        IEnumerable<IDomainEvent> GetEventsSinceLastSnapShot(Guid eventProviderId);
        int GetEventCountSinceLastSnapShot(Guid eventProviderId);
        void Save(IEventProvider eventProvider);
    }
}