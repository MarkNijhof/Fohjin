using Fohjin.DDD.Common;

namespace Test.Fohjin.DDD.TestUtilities
{
    public class TestSystemTimer : ISystemTimer
    {
        public void Trigger(Action value, int @in) => value();
    }
}
