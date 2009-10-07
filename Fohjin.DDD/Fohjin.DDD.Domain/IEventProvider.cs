using System;
using System.Collections.Generic;
using Fohjin.DDD.Events;

namespace Fohjin.DDD.Domain
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
}