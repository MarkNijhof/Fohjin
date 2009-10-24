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
    public static class SystemRandom
    {
        public static Func<int, int, int> Next = (min, max) => new Random().Next(min, max);
        public static void Reset()
        {
            Next = (min, max) => new Random().Next(min, max);
        }
    }
}