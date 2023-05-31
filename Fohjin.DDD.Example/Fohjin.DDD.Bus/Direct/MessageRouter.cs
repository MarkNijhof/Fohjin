using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Configuration;
using Fohjin.DDD.EventStore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fohjin.DDD.Bus.Direct
{
    public class MessageRouter : IRouteMessages
    {
        private static int _seed;
        private readonly int _id = _seed++;

        private ICommandHandlerHelper? _commandHandlerHelper;
        private IEventHandlerHelper? _eventHandlerHelper;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _log;

        public MessageRouter(
            IServiceProvider serviceProvider,
            ILogger<MessageRouter> log
            )
        {
            _serviceProvider = serviceProvider;
            _log = log;
        }

        public async Task<bool> RouteAsync(object message)
        {
            _log.LogInformation($"RouteAsync({{id}})> {{type}}: {{{nameof(message)}}}", _id, message.GetType(), message);
            var handled = false;
            if (message is ICommand command)
            {
                _commandHandlerHelper ??= _serviceProvider.GetRequiredService<ICommandHandlerHelper>();
                handled |= await _commandHandlerHelper.RouteAsync(command);
            }
            if (message is IDomainEvent @event)
            {
                _eventHandlerHelper ??= _serviceProvider.GetRequiredService<IEventHandlerHelper>();
                handled |= await _eventHandlerHelper.RouteAsync(@event);
            }

            if (!handled)
                _log.LogWarning($"RouteAsync({{id}})-NotHandled> {{type}}: {{{nameof(message)}}}", _id, message.GetType(), message);

            return handled;
        }
    }
}