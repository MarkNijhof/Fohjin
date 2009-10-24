using System;

namespace Fohjin.DDD.Services
{
    public class TheAccountDoesNotBelongToThisBankException : Exception
    {
        public TheAccountDoesNotBelongToThisBankException(string message) : base(message) { }
    }
}