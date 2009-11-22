using System;

namespace Fohjin.DDD.EventStore.Storage.Aggregate
{
    public class UnregisteredDomainEventException : Exception
    {
        public UnregisteredDomainEventException(string message) : base(message) {}
    }
}