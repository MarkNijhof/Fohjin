using System;

namespace Fohjin.EventStore.Infrastructure
{
    public class IlligalStateAssignmentException : Exception
    {
        public IlligalStateAssignmentException(string message) : base(message)
        {
        }
    }
}