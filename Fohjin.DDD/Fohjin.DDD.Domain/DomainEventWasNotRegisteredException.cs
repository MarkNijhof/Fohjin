using System;

namespace Fohjin.DDD.Domain
{
    public class DomainEventWasNotRegisteredException : Exception
    {
        public DomainEventWasNotRegisteredException(string message) : base(message) {}
    }
}