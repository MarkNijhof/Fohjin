using System;

namespace Fohjin.DDD.Domain.Account
{
    public class ClosedAccountException : Exception
    {
        public ClosedAccountException(string message) : base(message) { }
    }
}