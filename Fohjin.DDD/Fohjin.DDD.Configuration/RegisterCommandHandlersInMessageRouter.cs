using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fohjin.DDD.Bus.Direct;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using StructureMap;

namespace Fohjin.DDD.Configuration
{
    public class RegisterCommandHandlersInMessageRouter
    {
        public static void BootStrap()
        {
            new RegisterCommandHandlersInMessageRouter().RegisterRoutes(ObjectFactory.GetInstance<IRouteMessages>() as MessageRouter);
        }

        public void RegisterRoutes(MessageRouter messageRouter)
        {
            var createPublishAction = GetType().GetMethod("CreatePublishAction");
            var register = messageRouter.GetType().GetMethod("Register");

            var commands = GetCommands();
            var commandHandlers = GetCommandHandlers();

            foreach (var command in commands)
            {
                foreach (var commandHandler in commandHandlers[command])
                {
                    var instance = GetCorrectlyInjectedCommandHandler(commandHandler);
                    var action = CreateTheProperAction(command, createPublishAction, instance);
                    RegisterTheCreatedActionWithTheMessageRouter(messageRouter, command, register, action);
                }
            }
        }

        private static void RegisterTheCreatedActionWithTheMessageRouter(MessageRouter messageRouter, Type command, MethodInfo register, object action)
        {
            register.MakeGenericMethod(command).Invoke(messageRouter, new[] { action });
        }

        private object CreateTheProperAction(Type command, MethodInfo createPublishAction, object instance)
        {
            return createPublishAction.MakeGenericMethod(command, instance.GetType()).Invoke(this, new[] { instance });
        }

        private static object GetCorrectlyInjectedCommandHandler(Type commandHandler)
        {
            return ObjectFactory.GetInstance(commandHandler);
        }

        public Action<TMessage> CreatePublishAction<TMessage, TMessageHandler>(TMessageHandler messageHandler) 
            where TMessage : class 
            where TMessageHandler : ICommandHandler<TMessage>
        {
            return messageHandler.Execute;
        }

        public static IDictionary<Type, IList<Type>> GetCommandHandlers()
        {
            IDictionary<Type, IList<Type>> commands = new Dictionary<Type, IList<Type>>();
            typeof(ICommandHandler<>)
                .Assembly
                .GetExportedTypes()
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
                .ToList()
                .ForEach(x => AddItem(commands, x));
            return commands;
        }

        public static IEnumerable<Type> GetCommands()
        {
            return typeof(Command)
                .Assembly
                .GetExportedTypes()
                .Where(x => x.BaseType == typeof(Command))
                .ToList();
        }

        private static void AddItem(IDictionary<Type, IList<Type>> dictionary, Type type)
        {
            var command = type.GetInterfaces()
                .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICommandHandler<>))
                .First()
                .GetGenericArguments()
                .First();

            if (!dictionary.ContainsKey(command))
                dictionary.Add(command, new List<Type>());

            dictionary[command].Add(type);
        }
    }
}