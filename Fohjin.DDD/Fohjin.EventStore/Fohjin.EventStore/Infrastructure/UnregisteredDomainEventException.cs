using System;

namespace Fohjin.EventStore.Infrastructure
{
    public class UnregisteredDomainEventException : Exception
    {
        public UnregisteredDomainEventException(string message) : base(message)
        {
        }
    }
}