using System;

namespace Fohjin.DDD.EventStore.Aggregate
{
    public class UnregisteredDomainEventException : Exception
    {
        public UnregisteredDomainEventException(string message) : base(message) {}
    }
}