using Fohjin.DDD.CommandHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fohjin.DDD.Bus.Direct
{
    public class MessageRouter : IRouteMessages
    {
        private ICommandHandlerHelper _commandHandlerHelper;
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

        public async Task RouteAsync(object message)
        {
            _log.LogInformation($"RouteAsync> {{type}}: {{{nameof(message)}}}", message.GetType(), message);
            _commandHandlerHelper ??= _serviceProvider.GetRequiredService<ICommandHandlerHelper>();
            await _commandHandlerHelper.RouteAsync(message);
        }
    }
}