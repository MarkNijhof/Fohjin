using Fohjin.DDD.Commands;

namespace Fohjin.DDD.CommandHandlers
{
    public interface ICommandHandler
    {
        
    }
    public interface ICommandHandler<TCommand> : ICommandHandler where TCommand : class, ICommand
    {
        void Execute(TCommand command);
    }
}