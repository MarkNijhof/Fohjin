using System;

namespace Fohjin.DDD.Domain.Client
{
    public class NonExistingBankCardException : Exception
    {
        public NonExistingBankCardException(string message) : base(message) {}
    }
}