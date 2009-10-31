using System;

namespace Fohjin.DDD.Domain.Client
{
    public class NonExistingClientException : Exception
    {
        public NonExistingClientException(string message) : base(message) {}
    }
}