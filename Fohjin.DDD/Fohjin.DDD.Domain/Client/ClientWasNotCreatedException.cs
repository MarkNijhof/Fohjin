using System;

namespace Fohjin.DDD.Domain.Client
{
    public class ClientWasNotCreatedException : Exception
    {
        public ClientWasNotCreatedException(string message) : base(message) {}
    }
}