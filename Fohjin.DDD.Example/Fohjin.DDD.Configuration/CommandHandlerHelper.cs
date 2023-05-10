using Fohjin.DDD.CommandHandlers;
using Microsoft.Extensions.Logging;

namespace Fohjin.DDD.Configuration
{
    public class CommandHandlerHelper : ICommandHandlerHelper
    {
        private IDictionary<Type, IEnumerable<Type>> _handlersCache;
        private IEnumerable<Type> _commandCache;

        private readonly IEnumerable<ICommandHandler> _handlers;
        private readonly ILogger _log;

        public CommandHandlerHelper(
            IEnumerable<ICommandHandler> handlers,
            ILogger<CommandHandlerHelper> log
            )
        {
            _handlers = handlers;
            _log = log;
        }

        protected IDictionary<Type, IEnumerable<Type>> GetCommandHandlers() =>
            _handlersCache ??= _handlers.ToDictionary(
                t => t.GetType(),
                t => (from i in t.GetType().GetInterfaces()
                      where i.IsGenericType
                      where i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)
                      select i.GetGenericArguments().First()).ToList().AsEnumerable());

        protected IEnumerable<Type> GetCommands() =>
            _commandCache ??= GetCommandHandlers().SelectMany(i => i.Value).Distinct().ToList();

        public async Task RouteAsync(object message)
        {
            _log.LogInformation($"RouteAsync> {{type}}: {{{nameof(message)}}}", message.GetType(), message);
            var targetHandler = typeof(ICommandHandler<>).MakeGenericType(message.GetType());
            var selectedHandlers = _handlers.Where(i => i.GetType().IsAssignableTo(targetHandler));

            foreach(var handler in selectedHandlers)
            {
                _log.LogInformation($"RouteAsync -> {{{nameof(handler)}}} {{type}}: {{{nameof(message)}}}", handler, message.GetType(), message);
                await handler.ExecuteAsync(message);
            }
        }
    }
}