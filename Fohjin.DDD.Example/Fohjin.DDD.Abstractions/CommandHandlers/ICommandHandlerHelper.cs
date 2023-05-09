namespace Fohjin.DDD.CommandHandlers
{
    public interface ICommandHandlerHelper
    {
        IDictionary<Type, IEnumerable<Type>> GetCommandHandlers();
        IEnumerable<Type> GetCommands();
        Task RouteAsync(object message);
    }
}