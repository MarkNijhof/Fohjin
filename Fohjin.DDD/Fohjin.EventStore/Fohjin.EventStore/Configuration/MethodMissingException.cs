using System;

namespace Fohjin.EventStore.Configuration
{
    public class MethodMissingException : Exception
    {
        public MethodMissingException(string message) : base(message)
        {
        }
    }
}