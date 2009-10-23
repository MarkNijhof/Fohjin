using System;

namespace Fohjin.DDD.Domain.Exceptions
{
    public class AccountMustFirstBeEmptiedBeforeClosingException : Exception
    {
        public AccountMustFirstBeEmptiedBeforeClosingException(string message) : base(message) { }
    }
}