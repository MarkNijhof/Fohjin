using Fohjin.DDD.Commands;

namespace Fohjin.DDD.CommandHandlers
{
    public interface ICommandHandler<TCommand> where TCommand : class, ICommand
    {
        void Execute(TCommand command);
    }
}