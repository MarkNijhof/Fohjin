using Fohjin.DDD.Bus;

namespace Fohjin.DDD.CommandHandlers
{
    public interface ICommandHandler<TCommand> where TCommand : class, IMessage
    {
        void Execute(TCommand command);
    }
}