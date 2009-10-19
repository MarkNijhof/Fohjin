using System;

namespace Fohjin.DDD.Domain.Exceptions
{
    public class DomainEventWasNotRegisteredException : Exception
    {
        public DomainEventWasNotRegisteredException(string message) : base(message) {}
    }
}