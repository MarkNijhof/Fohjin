namespace Fohjin.DDD.EventHandlers
{
    public abstract class EventHandlerBase<TEvent> : IEventHandler<TEvent> where TEvent : class
    {
        public abstract Task ExecuteAsync(TEvent command);
        public async Task ExecuteAsync(object command) => await ExecuteAsync((TEvent)command);
    }
}