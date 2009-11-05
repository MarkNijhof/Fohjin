namespace Fohjin.DDD.EventStore.Bus
{
    public interface IEventHandler
    {
        void Execute(object theEvent);
    }

    public interface IEventHandler<TEvent> where TEvent : class
    {
        void Execute(TEvent theEvent);
    }
}