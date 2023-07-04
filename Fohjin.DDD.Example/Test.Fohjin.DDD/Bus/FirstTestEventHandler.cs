using Fohjin.DDD.Commands;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.EventStore;

namespace Test.Fohjin.DDD.Bus
{
    public class FirstTestEventHandler : IEventHandler<TestEvent>
    {
        public List<Guid> Ids;

        public FirstTestEventHandler()
        {
            Ids = new List<Guid>();
        }

        public Task ExecuteAsync(TestEvent @event)
        {
            Ids.Add(@event.Id);
            return Task.CompletedTask;
        }

        public Task ExecuteAsync(IDomainEvent @event) =>
            ExecuteAsync((TestEvent)@event);
    }
}