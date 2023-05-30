using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.EventStore;

namespace Test.Fohjin.DDD.Bus
{
    public class SecondTestEventHandler : IEventHandler<TestEvent>
    {
        public List<Guid> Ids;

        public SecondTestEventHandler()
        {
            Ids = new List<Guid>();
        }

        public Task ExecuteAsync(TestEvent command)
        {
            Ids.Add(command.Id);
            return Task.CompletedTask;
        }

        public Task ExecuteAsync(IDomainEvent @event) =>
            ExecuteAsync(@event as TestEvent);
    }
}