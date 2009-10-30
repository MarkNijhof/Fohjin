using System;

namespace Fohjin.DDD.Domain.Account
{
    public class AccountWasNotCreatedException : Exception
    {
        public AccountWasNotCreatedException(string message) : base(message) {}
    }
}