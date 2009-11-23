using System;

namespace Fohjin.DDD.Domain.Client
{
    public class BankCardIsDisabledException : Exception
    {
        public BankCardIsDisabledException(string message) : base(message) { }
    }
}