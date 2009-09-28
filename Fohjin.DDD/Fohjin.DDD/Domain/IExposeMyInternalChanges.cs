using System;
using System.Collections.Generic;
using Fohjin.DDD.Domain.Events;

namespace Fohjin.DDD.Domain
{
    public interface IExposeMyInternalChanges 
    {
        void LoadHistory(IEnumerable<IDomainEvent> domainEvents);
        IEnumerable<IDomainEvent> GetChanges();
        void Clear();
        Guid Id { get; }
    }
}