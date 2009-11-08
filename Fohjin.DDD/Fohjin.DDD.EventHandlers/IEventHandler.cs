namespace Fohjin.DDD.EventHandlers
{
    public interface IEventHandler<TEvent> where TEvent : class
    {
        void Execute(TEvent theEvent);
    }
}