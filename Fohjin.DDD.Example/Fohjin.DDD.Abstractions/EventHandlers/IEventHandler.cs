namespace Fohjin.DDD.EventHandlers
{
    public interface IEventHandler
    {
        Task ExecuteAsync(object @event);
    }
    public interface IEventHandler<TEvent> : IEventHandler where TEvent : class
    {
        Task ExecuteAsync(TEvent @event);
    }
}