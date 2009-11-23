using System;

namespace Fohjin.DDD.Domain.Account
{
    public class NonExitsingAccountException : Exception
    {
        public NonExitsingAccountException(string message) : base(message) {}
    }
}