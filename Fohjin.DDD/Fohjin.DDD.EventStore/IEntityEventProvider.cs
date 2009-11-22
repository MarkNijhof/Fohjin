using System;
using System.Collections.Generic;

namespace Fohjin.DDD.EventStore
{
    public interface IEntityEventProvider<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        void Clear();
        void LoadFromHistory(IEnumerable<TDomainEvent> domainEvents);
        void HookUpVersionProvider(Func<int> versionProvider);
        IEnumerable<TDomainEvent> GetChanges();
        Guid Id { get; }
    }
}