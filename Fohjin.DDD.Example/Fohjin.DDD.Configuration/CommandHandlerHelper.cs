using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;

namespace Fohjin.DDD.Configuration
{
    public class CommandHandlerHelper : ICommandHandlerHelper
    {
        private readonly IEnumerable<ICommand> _commands;

        public CommandHandlerHelper(IEnumerable<ICommand> commands)
        {
            _commands = commands;
        }

        public IDictionary<Type, IList<Type>> GetCommandHandlers()
        {
            IDictionary<Type, IList<Type>> commands = new Dictionary<Type, IList<Type>>();
            typeof(ICommandHandler<>)
                .Assembly
                .GetExportedTypes()
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
                .ToList()
                .ForEach(x => AddItem(commands, x));
            return commands;
        }

        public IEnumerable<Type> GetCommands() => _commands.Select(c => c.GetType());

        private void AddItem(IDictionary<Type, IList<Type>> dictionary, Type type)
        {
            var command = type.GetInterfaces()
                .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICommandHandler<>))
                .First()
                .GetGenericArguments()
                .First();

            if (!dictionary.ContainsKey(command))
                dictionary.Add(command, new List<Type>());

            dictionary[command].Add(type);
        }
    }
}