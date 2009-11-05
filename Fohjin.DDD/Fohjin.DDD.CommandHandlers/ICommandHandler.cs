using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public interface ICommandHandler<TCommand> where TCommand : class
    {
        void Execute(TCommand compensatingCommand);
    }
}