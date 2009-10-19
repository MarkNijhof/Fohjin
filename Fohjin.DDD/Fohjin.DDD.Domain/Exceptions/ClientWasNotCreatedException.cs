using System;

namespace Fohjin.DDD.Domain.Exceptions
{
    public class ClientWasNotCreatedException : Exception
    {
        public ClientWasNotCreatedException(string message) : base(message) {}
    }
}