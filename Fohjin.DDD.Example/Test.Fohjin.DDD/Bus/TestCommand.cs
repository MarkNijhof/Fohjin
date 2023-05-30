using Fohjin.DDD.Commands;

namespace Test.Fohjin.DDD.Bus
{
    public record TestCommand : Command
    {
        public TestCommand(Guid id) : base(id)
        {
        }
    }
}