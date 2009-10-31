using System;

namespace Fohjin.DDD.Domain.Account
{
    public class AccountBalanceNotZeroException : Exception
    {
        public AccountBalanceNotZeroException(string message) : base(message) { }
    }
}