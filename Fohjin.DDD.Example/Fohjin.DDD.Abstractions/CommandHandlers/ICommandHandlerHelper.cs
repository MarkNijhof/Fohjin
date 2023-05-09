namespace Fohjin.DDD.CommandHandlers
{
    public interface ICommandHandlerHelper
    {
        IDictionary<Type, IList<Type>> GetCommandHandlers();
        IEnumerable<Type> GetCommands();
    }
}