namespace Fohjin.DDD.Common
{
    public class SystemRandom : ISystemRandom
    {
        private readonly Random _rand = new();

        public int Next(int start, int end) => _rand.Next(start, end);
    }
}