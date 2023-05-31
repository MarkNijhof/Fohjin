namespace Fohjin.DDD.Bus.Direct
{
    public interface IRouteMessages
    {
        Task<bool> RouteAsync(object message);
    }
}