using System;

namespace Fohjin.DDD.Services
{
    public class AccountDoesNotExistException : Exception
    {
        public AccountDoesNotExistException(string message) : base(message) { }
    }
}