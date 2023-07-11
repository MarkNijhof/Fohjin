using Fohjin.DDD.EventStore;
using System.Text.Json;

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