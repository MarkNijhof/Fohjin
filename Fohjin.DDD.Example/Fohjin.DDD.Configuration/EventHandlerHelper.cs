using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.EventStore;
using Microsoft.Extensions.Logging;

namespace Fohjin.DDD.Configuration
{
    public class EventHandlerHelper : IEventHandlerHelper
    {
        private IDictionary<Type, IEnumerable<Type>>? _handlersCache;
        private IEnumerable<Type>? _commandCache;

        private readonly IEnumerable<IEventHandler> _handlers;
        private readonly ILogger _log;

        public EventHandlerHelper(
            IEnumerable<IEventHandler> handlers,
            ILogger<EventHandlerHelper> log
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
                      where i.GetGenericTypeDefinition() == typeof(IEventHandler<>)
                      select i.GetGenericArguments().First()).ToList().AsEnumerable());

        protected IEnumerable<Type> GetCommands() =>
            _commandCache ??= GetCommandHandlers().SelectMany(i => i.Value).Distinct().ToList();

        public async Task<bool> RouteAsync(IDomainEvent message)
        {
            _log.LogInformation($"RouteAsync> {{type}}: {{{nameof(message)}}}", message.GetType(), message);
            var targetHandler = typeof(IEventHandler<>).MakeGenericType(message.GetType());
            var selectedHandlers = _handlers.Where(i => i.GetType().IsAssignableTo(targetHandler));

            if (!selectedHandlers.Any()) return false;

            foreach (var handler in selectedHandlers)
            {
                _log.LogInformation($"RouteAsync -> {{{nameof(handler)}}} {{type}}: {{{nameof(message)}}}", handler, message.GetType(), message);
                await handler.ExecuteAsync(message);
            }

            return true;
        }
    }
}