using System;

namespace Fohjin.DDD.Services
{
    public class UnknownAccountException : Exception
    {
        public UnknownAccountException(string message) : base(message) { }
    }
}