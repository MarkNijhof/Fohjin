using Fohjin.DDD.Commands;

namespace Fohjin.DDD.CommandHandlers
{
    public interface ICommandHandlerHelper
    {
        Task<bool> RouteAsync(ICommand message);
    }
}