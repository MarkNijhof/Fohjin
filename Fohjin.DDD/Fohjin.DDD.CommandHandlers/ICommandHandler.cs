using Fohjin.DDD.Bus;

namespace Fohjin.DDD.CommandHandlers
{
    public interface ICommandHandler {}
    public interface ICommandHandler<TCommand> : ICommandHandler where TCommand : class, IMessage
    {
        void Execute(TCommand command);
    }
}