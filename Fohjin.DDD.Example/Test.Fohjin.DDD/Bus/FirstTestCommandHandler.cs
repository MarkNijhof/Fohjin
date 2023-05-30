using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;

namespace Test.Fohjin.DDD.Bus
{
    public class FirstTestCommandHandler : ICommandHandler<TestCommand>
    {
        public List<Guid> Ids;

        public FirstTestCommandHandler()
        {
            Ids = new List<Guid>();
        }

        public Task ExecuteAsync(TestCommand compensatingCommand)
        {
            Ids.Add(compensatingCommand.Id);
            return Task.CompletedTask;
        }

        public Task ExecuteAsync(ICommand command) =>
            ExecuteAsync(command as TestCommand);
    }
}