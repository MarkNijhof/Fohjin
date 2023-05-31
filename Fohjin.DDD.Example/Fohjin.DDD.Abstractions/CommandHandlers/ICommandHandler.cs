using Fohjin.DDD.Commands;

namespace Fohjin.DDD.CommandHandlers
{
    public interface ICommandHandler
    {
        Task ExecuteAsync(ICommand command);
    }
    public interface ICommandHandler<TCommand> : ICommandHandler where TCommand : class, ICommand
    {
        Task ExecuteAsync(TCommand command);
    }
}