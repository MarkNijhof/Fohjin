using System;

namespace Fohjin.DDD.Domain
{
    public class UnregisteredDomainEventException : Exception
    {
        public UnregisteredDomainEventException(string message) : base(message) {}
    }
}