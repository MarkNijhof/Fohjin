using System.Collections.Generic;
using Fohjin.DDD.Domain.Events;

namespace Fohjin.DDD.Domain.Repositories
{
    public class DomainEventStorage : IDomainEventStorage
    {
        private readonly List<IDomainEvent> _domainEvents;

        public DomainEventStorage()
        {
            _domainEvents = new List<IDomainEvent>();
        }

        public IEnumerable<IDomainEvent> GetEvents()
        {
            return _domainEvents.AsReadOnly();
        }

        public void AddEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}