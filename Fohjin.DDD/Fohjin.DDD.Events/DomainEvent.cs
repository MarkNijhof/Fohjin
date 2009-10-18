using System;

namespace Fohjin.DDD.Events
{
    [Serializable]
    public class DomainEvent : IDomainEvent
    {
        public Guid Id { get; private set; }
        int IDomainEvent.Version { get; set; }

        public DomainEvent()
        {
            Id = Guid.NewGuid();
        }
    }
}