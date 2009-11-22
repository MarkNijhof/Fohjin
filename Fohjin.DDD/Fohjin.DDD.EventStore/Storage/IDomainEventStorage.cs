using System;
using System.Collections.Generic;

namespace Fohjin.DDD.EventStore.Storage
{
    public interface IDomainEventStorage<TDomainEvent> : ISnapShotStorage<TDomainEvent>, ITransactional where TDomainEvent : IDomainEvent
    {
        IEnumerable<TDomainEvent> GetAllEvents(Guid eventProviderId);
        IEnumerable<TDomainEvent> GetEventsSinceLastSnapShot(Guid eventProviderId);
        int GetEventCountSinceLastSnapShot(Guid eventProviderId);
        void Save(IEventProvider<TDomainEvent> eventProvider);
    }
}