using Fohjin.DDD.Commands;

namespace Test.Fohjin.DDD.Bus
{
    public class TestCommand : Command
    {
        public TestCommand(Guid id) : base(id)
        {
        }
    }
}