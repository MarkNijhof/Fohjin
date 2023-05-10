using Fohjin.DDD.Commands;

namespace Fohjin.DDD.CommandHandlers
{
    public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
    {
        public abstract Task ExecuteAsync(TCommand command);
        public async Task ExecuteAsync(ICommand command) => await ExecuteAsync((TCommand)command);
    }
}