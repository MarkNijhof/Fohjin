namespace Fohjin.DDD.CommandHandlers
{
    public interface ICommandHandlerHelper
    {
        Task RouteAsync(object message);
    }
}