using Fohjin.DDD.Commands;

namespace Fohjin.DDD.CommandHandlers
{
    public interface ITransactionHandler
    {
        Task ExecuteAsync(object command, object commandHandler);
    }
    public interface ITransactionHandler<TCommand, TCommandHandler> : ITransactionHandler
        where TCommand : class, ICommand
        where TCommandHandler : ICommandHandler<TCommand>
    {
        Task ExecuteAsync(TCommand command, TCommandHandler commandHandler);
    }
}