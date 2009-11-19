using System;

namespace Fohjin
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