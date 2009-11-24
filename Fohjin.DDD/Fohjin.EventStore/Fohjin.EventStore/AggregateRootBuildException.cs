using System;

namespace Fohjin.EventStore
{
    public class AggregateRootBuildException : Exception
    {
        public AggregateRootBuildException(string message) : base(message)
        {
        }
    }
}