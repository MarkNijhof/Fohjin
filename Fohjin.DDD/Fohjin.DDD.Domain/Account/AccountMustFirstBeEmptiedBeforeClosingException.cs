using System;

namespace Fohjin.DDD.Domain.Account
{
    public class AccountMustFirstBeEmptiedBeforeClosingException : Exception
    {
        public AccountMustFirstBeEmptiedBeforeClosingException(string message) : base(message) { }
    }
}