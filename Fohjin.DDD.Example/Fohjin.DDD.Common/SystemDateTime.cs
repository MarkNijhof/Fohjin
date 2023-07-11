namespace Fohjin.DDD.Common
{
    public class SystemDateTime : ISystemDateTime
    {
        public DateTimeOffset Now() => DateTimeOffset.Now;
    }
}