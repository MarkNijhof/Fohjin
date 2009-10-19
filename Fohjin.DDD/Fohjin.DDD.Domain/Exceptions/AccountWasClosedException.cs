using System;

namespace Fohjin.DDD.Domain.Exceptions
{
    public class AccountWasClosedException : Exception
    {
        public AccountWasClosedException(string message) : base(message) { }
    }
}