using System;

namespace Fohjin.DDD.Domain.Client
{
    public class NonExistingAccountException : Exception
    {
        public NonExistingAccountException(string message) : base(message) { }
    }
}