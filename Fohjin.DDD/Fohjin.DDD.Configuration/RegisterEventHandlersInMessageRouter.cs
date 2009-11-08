using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fohjin.DDD.Bus.Direct;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events;
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

            var events = GetEvents();
            var eventHandlers = GetEventHandlers();

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

        public Action<TMessage> CreatePublishAction<TMessage, TMessageHandler>(TMessageHandler messageHandler)
            where TMessage : class
            where TMessageHandler : IEventHandler<TMessage>
        {
            return messageHandler.Execute;
        }

        public static IDictionary<Type, IList<Type>> GetEventHandlers()
        {
            var commands = new Dictionary<Type, IList<Type>>();
            typeof(ClientCreatedEventHandler)
                .Assembly
                .GetExportedTypes()
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IEventHandler<>)))
                .ToList()
                .ForEach(x => AddItem(commands, x));
            return commands;
        }

        public static IEnumerable<Type> GetEvents()
        {
            return typeof(DomainEvent)
                .Assembly
                .GetExportedTypes()
                .Where(x => x.BaseType == typeof(DomainEvent))
                .ToList();
        }

        private static void AddItem(IDictionary<Type, IList<Type>> dictionary, Type type)
        {
            var theEvent = type.GetInterfaces()
                .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEventHandler<>))
                .First()
                .GetGenericArguments()
                .First();

            if (!dictionary.ContainsKey(theEvent))
                dictionary.Add(theEvent, new List<Type>());

            dictionary[theEvent].Add(type);
        }
    }
}