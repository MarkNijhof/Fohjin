using Fohjin.DDD.Bus.Direct;
using Fohjin.DDD.EventHandlers;
using System.Reflection;

namespace Fohjin.DDD.Configuration
{
    public class RegisterEventHandlersInMessageRouter
    {
        private static MethodInfo _createPublishActionMethod;
        private static MethodInfo _registerMethod;

        private readonly IServiceProvider _serviceProvider;
        private readonly IRouteMessages _routeMessages;


        public RegisterEventHandlersInMessageRouter(
            IServiceProvider serviceProvider,
            IRouteMessages routeMessages
            )
        {
            _serviceProvider = serviceProvider;
            _routeMessages = routeMessages;
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            _createPublishActionMethod ??= GetType().GetMethod("CreatePublishAction");
            _registerMethod ??= _routeMessages.GetType().GetMethod("Register");

            var events = EventHandlerHelper.GetEvents();
            var eventHandlers = EventHandlerHelper.GetEventHandlers();

            foreach (var theEvent in events)
            {
                if (!eventHandlers.TryGetValue(theEvent, out IList<Type> eventHandlerTypes))
                    throw new Exception(string.Format("No event handlers found for event '{0}'", theEvent.FullName));

                foreach (var eventHandler in eventHandlerTypes)
                {
                    var injectedEventHandler = GetCorrectlyInjectedEventHandler(eventHandler);
                    var action = CreateTheProperAction(theEvent, injectedEventHandler);
                    RegisterTheCreatedActionWithTheMessageRouter(_routeMessages, theEvent, action);
                }
            }
        }

        public Action<TMessage> CreatePublishAction<TMessage, TMessageHandler>(TMessageHandler messageHandler)
            where TMessage : class
            where TMessageHandler : IEventHandler<TMessage>
        {
            return messageHandler.Execute;
        }

        private static void RegisterTheCreatedActionWithTheMessageRouter(IRouteMessages messageRouter, Type eventType, object action) =>
            _registerMethod.MakeGenericMethod(eventType).Invoke(messageRouter, new[] { action });

        private object CreateTheProperAction(Type eventType, object eventHandler) =>
            _createPublishActionMethod.MakeGenericMethod(eventType, eventHandler.GetType()).Invoke(this, new[] { eventHandler });

        private object GetCorrectlyInjectedEventHandler(Type eventHandler) =>
            _serviceProvider.GetService(eventHandler)
                ?? throw new ApplicationException($"unable to locate handler for {eventHandler}");
    }
}