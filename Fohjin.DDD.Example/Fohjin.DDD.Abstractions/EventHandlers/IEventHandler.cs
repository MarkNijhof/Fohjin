using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.EventHandlers
{
    public interface IEventHandler
    {
        Task ExecuteAsync(IDomainEvent @event);
    }
    public interface IEventHandler<TEvent> : IEventHandler where TEvent : class, IDomainEvent
    {
        Task ExecuteAsync(TEvent @event);
    }
}