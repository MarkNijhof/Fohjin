namespace Fohjin.DDD.CommandHandlers
{
    public interface ITransactionHandler<TCommand, TCommandHandler>
        where TCommand : class
        where TCommandHandler : ICommandHandler<TCommand>
    {
        Task ExecuteAsync(TCommand command, TCommandHandler commandHandler);
    }
}