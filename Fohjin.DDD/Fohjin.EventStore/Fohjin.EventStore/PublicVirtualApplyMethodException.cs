using System;

namespace Fohjin.EventStore
{
    public class PublicVirtualApplyMethodException : Exception
    {
        public PublicVirtualApplyMethodException(string message) : base(message)
        {
        }
    }
}