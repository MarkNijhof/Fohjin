using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.EventHandlers
{
    public interface IEventHandler<TEvent> where TEvent : class, IMessage
    {
        void Execute(TEvent theEvent);
    }
}