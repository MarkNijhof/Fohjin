using System;

namespace Fohjin.DDD.Domain.Account
{
    public class AccountWasClosedException : Exception
    {
        public AccountWasClosedException(string message) : base(message) { }
    }
}