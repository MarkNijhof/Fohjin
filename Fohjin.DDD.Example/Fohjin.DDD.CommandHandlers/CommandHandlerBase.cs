namespace Fohjin.DDD.CommandHandlers
{
    public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand> where TCommand : class
    {
        public abstract Task ExecuteAsync(TCommand command);
        public async Task ExecuteAsync(object command) => await ExecuteAsync((TCommand)command);
    }
}