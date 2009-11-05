using System;
using System.Collections.Generic;

namespace Fohjin.DDD.EventStore
{
    public interface ITransactional
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }

    public interface IDomainEventStorage : ISnapShotStorage, ITransactional
    {
        IEnumerable<IDomainEvent> GetAllEvents(Guid eventProviderId);
        IEnumerable<IDomainEvent> GetEventsSinceLastSnapShot(Guid eventProviderId);
        int GetEventCountSinceLastSnapShot(Guid eventProviderId);
        void Save(IEventProvider eventProvider);
    }
}