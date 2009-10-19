using Fohjin.DDD.Bus;

namespace Fohjin.DDD.EventHandlers
{
    public interface IEventHandler<TCommand> where TCommand : class, IMessage
    {
        void Execute(TCommand command);
    }
}