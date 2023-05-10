namespace Fohjin.DDD.Configuration
{
    public interface IEventHandlerHelper
    {
        Task RouteAsync(object message);
    }
}