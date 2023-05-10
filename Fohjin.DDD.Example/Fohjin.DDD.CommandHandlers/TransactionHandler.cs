using Fohjin.DDD.EventStore;
using Microsoft.Extensions.Logging;

namespace Fohjin.DDD.CommandHandlers
{
    public class TransactionHandler<TCommand, TCommandHandler> :
        ITransactionHandler<TCommand, TCommandHandler>
        where TCommandHandler : CommandHandlerBase<TCommand>
        where TCommand : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _log;

        public TransactionHandler(
            IUnitOfWork unitOfWork,
            ILogger<TransactionHandler<TCommand, TCommandHandler>> log
            )
        {
            // EventStoreUnitOfWork<TDomainEvent> : IEventStoreUnitOfWork<TDomainEvent>
            _unitOfWork = unitOfWork;
            _log = log;
        }

        public async Task ExecuteAsync(TCommand command, TCommandHandler commandHandler)
        {
            _log.LogInformation($"{nameof(ExecuteAsync)}> {{{nameof(command)}}}, {{{nameof(commandHandler)}}}", command, commandHandler);
            try
            {
                await commandHandler.ExecuteAsync(command);
                _log.LogInformation($"{nameof(ExecuteAsync)}-{nameof(_unitOfWork.Commit)}> {{{nameof(command)}}}, {{{nameof(commandHandler)}}}", command, commandHandler);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _log.LogError($"{nameof(ExecuteAsync)}-{nameof(_unitOfWork.Rollback)}> {{{nameof(command)}}}, {{{nameof(commandHandler)}}}-{{{nameof(ex.Message)}}}", command, commandHandler, ex.Message);
                _unitOfWork.Rollback();
                throw;
            }
        }

        public Task ExecuteAsync(object command, object commandHandler) =>
            ExecuteAsync((TCommand)command, (TCommandHandler)commandHandler);
    }
}