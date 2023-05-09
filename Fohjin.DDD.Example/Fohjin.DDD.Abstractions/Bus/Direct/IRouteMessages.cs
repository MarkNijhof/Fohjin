namespace Fohjin.DDD.Bus.Direct
{
    public interface IRouteMessages
    {
        Task RouteAsync(object message);
    }
}