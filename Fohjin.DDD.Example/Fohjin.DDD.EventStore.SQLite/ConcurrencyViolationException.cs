namespace Fohjin.DDD.EventStore.SQLite
{
    public class ConcurrencyViolationException : Exception
    {
        public ConcurrencyViolationException(string? message) : base(message)
        {
        }
    }
}