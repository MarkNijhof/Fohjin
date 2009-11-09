using System;
using System.Reflection;
using Fohjin.DDD.Bus.Direct;
using Fohjin.DDD.EventHandlers;
using StructureMap;

namespace Fohjin.DDD.Configuration
{
    public class RegisterEventHandlersInMessageRouter
    {
        public static void BootStrap()
        {
            new RegisterEventHandlersInMessageRouter().RegisterRoutes(ObjectFactory.GetInstance<IRouteMessages>() as MessageRouter);
        }

        public void RegisterRoutes(MessageRouter messageRouter)
        {
            var createPublishAction = GetType().GetMethod("CreatePublishAction");
            var register = messageRouter.GetType().GetMethod("Register");

            var events = EventHandlerHelper.GetEvents();
            var eventHandlers = EventHandlerHelper.GetEventHandlers();

            foreach (var theEvent in events)
            {
                foreach (var eventHandler in eventHandlers[theEvent])
                {
                    var instance = GetCorrectlyInjectedEventHandler(eventHandler);
                    var action = CreateTheProperAction(theEvent, createPublishAction, instance);
                    RegisterTheCreatedActionWithTheMessageRouter(messageRouter, theEvent, register, action);
                }
            }
        }

        public Action<TMessage> CreatePublishAction<TMessage, TMessageHandler>(TMessageHandler messageHandler)
            where TMessage : class
            where TMessageHandler : IEventHandler<TMessage>
        {
            return messageHandler.Execute;
        }

        private static void RegisterTheCreatedActionWithTheMessageRouter(MessageRouter messageRouter, Type theEvent, MethodInfo register, object action)
        {
            register.MakeGenericMethod(theEvent).Invoke(messageRouter, new[] { action });
        }

        private object CreateTheProperAction(Type theEvent, MethodInfo createPublishAction, object instance)
        {
            return createPublishAction.MakeGenericMethod(theEvent, instance.GetType()).Invoke(this, new[] { instance });
        }

        private static object GetCorrectlyInjectedEventHandler(Type eventHandler)
        {
            return ObjectFactory.GetInstance(eventHandler);
        }
    }
}