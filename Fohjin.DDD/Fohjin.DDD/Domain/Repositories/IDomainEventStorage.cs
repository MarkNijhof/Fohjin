using System.Collections.Generic;
using Fohjin.DDD.Domain.Events;

namespace Fohjin.DDD.Domain.Repositories
{
    public interface IDomainEventStorage
    {
        IEnumerable<IDomainEvent> GetEvents();
        void AddEvent(IDomainEvent domainEvent);
    }
}