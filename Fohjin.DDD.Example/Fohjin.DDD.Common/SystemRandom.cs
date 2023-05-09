namespace Fohjin.DDD.Common
{
    public class SystemRandom : ISystemRandom
    {
        private readonly Random _rand = new Random();

        public int Next(int start, int end) => _rand.Next(start, end);
    }
}