using System;

namespace Fohjin.EventStore
{
    public class UnregisteredDomainEventException : Exception
    {
        public UnregisteredDomainEventException(string message) : base(message)
        {
        }
    }
}