using Fohjin.DDD.CommandHandlers;

namespace Fohjin.DDD.Configuration
{
    public class CommandHandlerHelper : ICommandHandlerHelper
    {
        private IDictionary<Type, IEnumerable<Type>> _handlersCache;
        private IEnumerable<Type> _commandCache;

        private readonly IEnumerable<ICommandHandler> _handlers;

        public CommandHandlerHelper(
            IEnumerable<ICommandHandler> handlers
            )
        {
            _handlers = handlers;
        }

        public IDictionary<Type, IEnumerable<Type>> GetCommandHandlers() =>
            _handlersCache ??= _handlers.ToDictionary(
                t => t.GetType(),
                t => (from i in t.GetType().GetInterfaces()
                      where i.IsGenericType
                      where i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)
                      select i.GetGenericArguments().First()).ToList().AsEnumerable());

        public IEnumerable<Type> GetCommands() =>
            _commandCache ??= GetCommandHandlers().SelectMany(i => i.Value).Distinct().ToList();

        public async Task RouteAsync(object message)
        {
            var targetHandler = typeof(ICommandHandler<>).MakeGenericType(message.GetType());
            var selectedHandlers = _handlers.Where(i => i.GetType().IsAssignableTo(targetHandler));

            foreach(var handler in selectedHandlers)
            {
                await handler.ExecuteAsync(message);
            }
        }
    }
}