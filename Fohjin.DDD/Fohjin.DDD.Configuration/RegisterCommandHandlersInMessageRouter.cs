using System;
using System.Reflection;
using Fohjin.DDD.Bus.Direct;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.EventStore;
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
            var createPublishAction = GetType().GetMethod("CreatePublishActionWrappedInTransaction");
            var register = messageRouter.GetType().GetMethod("Register");

            var commands = CommandHandlerHelper.GetCommands();
            var commandHandlers = CommandHandlerHelper.GetCommandHandlers();

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
            var unitOfWork = ObjectFactory.GetInstance<IUnitOfWork>();
            return createPublishAction.MakeGenericMethod(command, instance.GetType()).Invoke(this, new[] { unitOfWork, instance });
        }

        private static object GetCorrectlyInjectedCommandHandler(Type commandHandler)
        {
            return ObjectFactory.GetInstance(commandHandler);
        }

        public Action<TMessage> CreatePublishActionWrappedInTransaction<TMessage, TMessageHandler>(IUnitOfWork unitOfWork, TMessageHandler messageHandler) 
            where TMessage : class 
            where TMessageHandler : ICommandHandler<TMessage>
        {
            return message => ObjectFactory.GetInstance<TransactionHandler<TMessage, TMessageHandler>>().Execute(message, messageHandler);
        }
    }
}