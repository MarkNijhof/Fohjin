using System;

namespace Fohjin.DDD.Domain.Exceptions
{
    public class AccountWasNotCreatedException : Exception
    {
        public AccountWasNotCreatedException(string message) : base(message) {}
    }
}