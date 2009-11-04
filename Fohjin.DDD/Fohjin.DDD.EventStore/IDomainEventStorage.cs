using System;
using System.Collections.Generic;

namespace Fohjin.DDD.EventStore
{
    public interface IDomainEventStorage : ISnapShotStorage
    {
        IEnumerable<IDomainEvent> GetAllEvents(Guid eventProviderId);
        IEnumerable<IDomainEvent> GetEventsSinceLastSnapShot(Guid eventProviderId);
        int GetEventCountSinceLastSnapShot(Guid eventProviderId);
        void Save(IEventProvider eventProvider);
    }
}