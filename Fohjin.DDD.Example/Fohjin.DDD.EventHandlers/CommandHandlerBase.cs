using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.EventHandlers
{
    public abstract class EventHandlerBase<TEvent> : IEventHandler<TEvent> where TEvent : class, IDomainEvent
    {
        public abstract Task ExecuteAsync(TEvent command);
        public async Task ExecuteAsync(IDomainEvent command) => await ExecuteAsync((TEvent)command);
    }
}