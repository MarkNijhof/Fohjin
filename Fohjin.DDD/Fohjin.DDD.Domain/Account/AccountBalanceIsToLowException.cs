using System;

namespace Fohjin.DDD.Domain.Account
{
    public class AccountBalanceIsToLowException : Exception
    {
        public AccountBalanceIsToLowException(string message) : base(message) { }
    }
}