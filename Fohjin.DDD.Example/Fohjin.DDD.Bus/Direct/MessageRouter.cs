using Fohjin.DDD.CommandHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace Fohjin.DDD.Bus.Direct
{
    public class MessageRouter : IRouteMessages
    {
        private ICommandHandlerHelper _commandHandlerHelper;
        private readonly IServiceProvider _serviceProvider;

        public MessageRouter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        //public void Register<TMessage>(Action<TMessage> route) where TMessage : class
        //{
        //    var routingKey = typeof(TMessage);

        //    if (!_routes.TryGetValue(routingKey, out var routes))
        //        _routes[routingKey] = routes = new LinkedList<Action<object>>();

        //    routes.Add(message => route(message as TMessage));
        //}

        public async Task RouteAsync(object message)
        {
            var messageType = message.GetType();
            _commandHandlerHelper ??= _serviceProvider.GetRequiredService<ICommandHandlerHelper>();

            await _commandHandlerHelper.RouteAsync(message);
        }
    }
}