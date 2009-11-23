using System;

namespace Fohjin.DDD.Domain.Account
{
    public class AccountBalanceToLowException : Exception
    {
        public AccountBalanceToLowException(string message) : base(message) { }
    }
}