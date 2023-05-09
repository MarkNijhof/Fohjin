using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public class TransactionHandler<TCommand, TCommandHandler> :
        ITransactionHandler<TCommand, TCommandHandler>
        where TCommandHandler : CommandHandlerBase<TCommand>
        where TCommand : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(TCommand command, TCommandHandler commandHandler)
        {
            try
            {
                await commandHandler.ExecuteAsync(command);
                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}