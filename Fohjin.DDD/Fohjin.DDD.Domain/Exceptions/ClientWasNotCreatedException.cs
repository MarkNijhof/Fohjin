using System;
using System.Runtime.Serialization;

namespace Fohjin.DDD.Domain.Exceptions
{
    public class ClientWasNotCreatedException : Exception
    {
        public ClientWasNotCreatedException() {}
        public ClientWasNotCreatedException(string message) : base(message) {}
        public ClientWasNotCreatedException(string message, Exception innerException) : base(message, innerException) {}
        protected ClientWasNotCreatedException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    }
}