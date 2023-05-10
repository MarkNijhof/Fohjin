using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Fohjin.DDD.Configuration
{
    public class CommandHandlerHelper : ICommandHandlerHelper
    {
        private IDictionary<Type, IEnumerable<Type>> _handlersCache;
        private IEnumerable<Type> _commandCache;

        private readonly IEnumerable<ICommandHandler> _handlers;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _log;

        public CommandHandlerHelper(
            IEnumerable<ICommandHandler> handlers,
            IServiceProvider serviceProvider,
            ILogger<CommandHandlerHelper> log
            )
        {
            _handlers = handlers;
            _serviceProvider = serviceProvider;
            _log = log;
        }

        protected IDictionary<Type, IEnumerable<Type>> GetCommandHandlers() =>
            _handlersCache ??= _handlers.ToDictionary(
                t => t.GetType(),
                t => (from i in t.GetType().GetInterfaces()
                      where i.IsGenericType
                      where i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)
                      select i.GetGenericArguments().First()).ToList().AsEnumerable());

        protected IEnumerable<Type> GetCommands() =>
            _commandCache ??= GetCommandHandlers().SelectMany(i => i.Value).Distinct().ToList();

        public async Task<bool> RouteAsync(ICommand message)
        {
            _log.LogInformation($"RouteAsync> {{type}}: {{{nameof(message)}}}", message.GetType(), message);
            var targetHandler = typeof(ICommandHandler<>).MakeGenericType(message.GetType());
            var selectedHandlers = _handlers.Where(i => i.GetType().IsAssignableTo(targetHandler));

            if (!selectedHandlers.Any()) return false;

            foreach (var handler in selectedHandlers)
            {
                _log.LogInformation($"RouteAsync -> {{{nameof(handler)}}} {{type}}: {{{nameof(message)}}}", handler, message.GetType(), message);

                var transactionHandlerType = typeof(ITransactionHandler<,>).MakeGenericType(message.GetType(), handler.GetType());
                var transactionHandler = (ITransactionHandler)_serviceProvider.GetRequiredService(transactionHandlerType);

                await transactionHandler.ExecuteAsync(message, handler);
            }
            return true;
        }
    }
}