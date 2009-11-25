using System;

namespace Fohjin.EventStore.Infrastructure
{
    public class ProtectedApplyMethodMissingException : Exception
    {
        public ProtectedApplyMethodMissingException(string message) : base(message)
        {
        }
    }
}