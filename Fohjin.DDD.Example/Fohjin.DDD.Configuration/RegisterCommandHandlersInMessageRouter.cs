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

        private readonly IServiceProvider _serviceProvider;
        private readonly IRouteMessages _routeMessages;
        private readonly ICommandHandlerHelper _commandHandlerHelper;

        public RegisterCommandHandlersInMessageRouter(
            IServiceProvider serviceProvider,
            IRouteMessages routeMessages,
            ICommandHandlerHelper commandHandlerHelper
            )
        {
            _serviceProvider = serviceProvider;
            _routeMessages = routeMessages;
            _commandHandlerHelper = commandHandlerHelper;

            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            _createPublishActionWrappedInTransactionMethod = GetType().GetMethod("CreatePublishActionWrappedInTransaction");
            _registerMethod = _routeMessages.GetType().GetMethod("Register");

            var commands = _commandHandlerHelper.GetCommands();
            var commandHandlers = _commandHandlerHelper.GetCommandHandlers();

            foreach (var command in commands)
            {
                if (!commandHandlers.TryGetValue(command, out var commandHandlerTypes))
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
            return command => _serviceProvider.GetService<ITransactionHandler<TCommand, TCommandHandler>>()
            .ExecuteAsync(command, commandHandler)
            .GetAwaiter().GetResult();
        }
    }
}