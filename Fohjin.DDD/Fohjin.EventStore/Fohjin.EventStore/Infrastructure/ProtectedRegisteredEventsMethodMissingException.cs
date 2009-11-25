using System;

namespace Fohjin.EventStore.Infrastructure
{
    public class ProtectedRegisteredEventsMethodMissingException : Exception
    {
        public ProtectedRegisteredEventsMethodMissingException(string message) : base(message)
        {
        }
    }
}