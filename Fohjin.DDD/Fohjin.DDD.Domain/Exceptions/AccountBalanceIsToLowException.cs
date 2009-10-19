using System;

namespace Fohjin.DDD.Domain.Exceptions
{
    public class AccountBalanceIsToLowException : Exception
    {
        public AccountBalanceIsToLowException(string message) : base(message) { }
    }
}