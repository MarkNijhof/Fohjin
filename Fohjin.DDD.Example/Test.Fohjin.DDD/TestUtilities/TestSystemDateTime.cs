using Fohjin.DDD.Common;

namespace Test.Fohjin.DDD.TestUtilities
{
    public class TestSystemDateTime : ISystemDateTime
    {
        private readonly DateTimeOffset _now;

        public TestSystemDateTime(
             DateTimeOffset now)
        {
            _now = now;
        }
        public DateTimeOffset Now() => _now;
    }
}
