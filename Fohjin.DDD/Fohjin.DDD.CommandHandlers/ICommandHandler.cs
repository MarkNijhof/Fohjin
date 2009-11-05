using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public interface ICommandHandler<TCommand> where TCommand : class, IMessage
    {
        void Execute(TCommand compensatingCommand);
    }
}