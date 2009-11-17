using System;
using System.Collections.Generic;

namespace Fohjin.DDD.EventStore
{
    public interface IEntityEventProvider 
    {
        void Clear();
        void LoadFromHistory(IEnumerable<IDomainEvent> domainEvents);
        void HookUpVersionProvider(Func<int> versionProvider);
        IEnumerable<IDomainEvent> GetChanges();
        Guid Id { get; }
    }

    public interface IEventProvider
    {
        void Clear();
        void LoadFromHistory(IEnumerable<IDomainEvent> domainEvents);
        void UpdateVersion(int version);
        void RegisterChildEventProvider(IEntityEventProvider entityEventProvider);
        Guid Id { get; }
        int Version { get; }
        IEnumerable<IDomainEvent> GetChanges();
    }
}