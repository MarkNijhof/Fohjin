using System;
using System.Collections.Generic;
using System.Reflection;
using Fohjin.DDD.Bus.Direct;
using Fohjin.DDD.EventHandlers;
using StructureMap;

namespace Fohjin.DDD.Configuration
{
    public class RegisterEventHandlersInMessageRouter
    {
        private static MethodInfo _createPublishActionMethod;
        private static MethodInfo _registerMethod;

        public static void BootStrap()
        {
            new RegisterEventHandlersInMessageRouter().RegisterRoutes(ObjectFactory.GetInstance<IRouteMessages>() as MessageRouter);
        }

        public void RegisterRoutes(MessageRouter messageRouter)
        {
            _createPublishActionMethod = GetType().GetMethod("CreatePublishAction");
            _registerMethod = messageRouter.GetType().GetMethod("Register");

            var events = EventHandlerHelper.GetEvents();
            var eventHandlers = EventHandlerHelper.GetEventHandlers();

            foreach (var theEvent in events)
            {
                IList<Type> eventHandlerTypes;
                if (!eventHandlers.TryGetValue(theEvent, out eventHandlerTypes))
                    throw new Exception(string.Format("No event handlers found for event '{0}'", theEvent.FullName));

                foreach (var eventHandler in eventHandlerTypes)
                {
                    var injectedEventHandler = GetCorrectlyInjectedEventHandler(eventHandler);
                    var action = CreateTheProperAction(theEvent, injectedEventHandler);
                    RegisterTheCreatedActionWithTheMessageRouter(messageRouter, theEvent, action);
                }
            }
        }

        public Action<TMessage> CreatePublishAction<TMessage, TMessageHandler>(TMessageHandler messageHandler)
            where TMessage : class
            where TMessageHandler : IEventHandler<TMessage>
        {
            return messageHandler.Execute;
        }

        private static void RegisterTheCreatedActionWithTheMessageRouter(MessageRouter messageRouter, Type eventType, object action)
        {
            _registerMethod.MakeGenericMethod(eventType).Invoke(messageRouter, new[] { action });
        }

        private object CreateTheProperAction(Type eventType, object eventHandler)
        {
            return _createPublishActionMethod.MakeGenericMethod(eventType, eventHandler.GetType()).Invoke(this, new[] { eventHandler });
        }

        private static object GetCorrectlyInjectedEventHandler(Type eventHandler)
        {
            return ObjectFactory.GetInstance(eventHandler);
        }
    }
}