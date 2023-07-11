using Fohjin.DDD.Common;

namespace Test.Fohjin.DDD.TestUtilities
{
    public class TestSystemRandom : ISystemRandom
    {
        private readonly Func<int, int, int> _rand;

        public TestSystemRandom(
             Func<int, int, int> rand)
        {
            _rand = rand;
        }

        public int Next(int start, int end) => _rand(start, end);
    }
}
