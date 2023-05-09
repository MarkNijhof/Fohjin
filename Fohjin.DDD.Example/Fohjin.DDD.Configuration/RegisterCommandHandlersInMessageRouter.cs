using Fohjin.DDD.Bus.Direct;
using Fohjin.DDD.CommandHandlers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Fohjin.DDD.Configuration
{
    public class RegisterCommandHandlersInMessageRouter
    {
        private static MethodInfo _createPublishActionWrappedInTransactionMethod;
        private static MethodInfo _registerMethod;

        private IServiceProvider _serviceProvider;
        private IRouteMessages _routeMessages;

        public RegisterCommandHandlersInMessageRouter(
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
            _createPublishActionWrappedInTransactionMethod = GetType().GetMethod("CreatePublishActionWrappedInTransaction");
            _registerMethod = _routeMessages.GetType().GetMethod("Register");

            var commands = CommandHandlerHelper.GetCommands();
            var commandHandlers = CommandHandlerHelper.GetCommandHandlers();

            foreach (var command in commands)
            {
                IList<Type> commandHandlerTypes;
                if (!commandHandlers.TryGetValue(command, out commandHandlerTypes))
                    throw new Exception(string.Format("No command handlers found for event '{0}'", command.FullName));

                foreach (var commandHandler in commandHandlerTypes)
                {
                    var injectedCommandHandler = GetCorrectlyInjectedCommandHandler(commandHandler);
                    var action = CreateTheProperAction(command, injectedCommandHandler);
                    RegisterTheCreatedActionWithTheMessageRouter(_routeMessages, command, action);
                }
            }
        }

        private object GetCorrectlyInjectedCommandHandler(Type commandHandler) =>
            _serviceProvider.GetService(commandHandler);

        private static void RegisterTheCreatedActionWithTheMessageRouter(IRouteMessages messageRouter, Type commandType, object action) =>
            _registerMethod.MakeGenericMethod(commandType).Invoke(messageRouter, new[] { action });

        private object CreateTheProperAction(Type commandType, object commandHandler) =>
            _createPublishActionWrappedInTransactionMethod.MakeGenericMethod(commandType, commandHandler.GetType()).Invoke(this, new[] { commandHandler });

        public Action<TCommand> CreatePublishActionWrappedInTransaction<TCommand, TCommandHandler>(TCommandHandler commandHandler)
            where TCommand : class
            where TCommandHandler : ICommandHandler<TCommand>
        {
            return command => _serviceProvider.GetService<TransactionHandler<TCommand, TCommandHandler>>().Execute(command, commandHandler);
        }
    }
}