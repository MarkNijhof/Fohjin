using System;
using System.Collections.Generic;

namespace Fohjin.DDD.EventStore
{
    public interface IEventProvider 
    {
        void LoadFromHistory(IEnumerable<IDomainEvent> domainEvents);
        IEnumerable<IDomainEvent> GetChanges();
        void Clear();
        void UpdateVersion(int version);
        Guid Id { get; }
        int Version { get; }
    }

    public interface IAggregateRootEventProvider : IEventProvider
    {
        void RegisterChildEventProvider(IEventProvider eventProvider);
    }
}