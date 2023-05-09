namespace Fohjin.DDD.CommandHandlers
{
    public interface ICommandHandler
    {
        Task ExecuteAsync(object command);
    }
    public interface ICommandHandler<TCommand> : ICommandHandler where TCommand : class
    {
        Task ExecuteAsync(TCommand command);
    }
}