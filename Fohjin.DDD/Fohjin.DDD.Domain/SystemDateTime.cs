using System;

namespace Fohjin.DDD.Domain
{
    public static class SystemDateTime
    {
        public static Func<DateTime> Now = () => DateTime.Now;
        public static void Reset()
        {
            Now = () => DateTime.Now;
        }
    }
}