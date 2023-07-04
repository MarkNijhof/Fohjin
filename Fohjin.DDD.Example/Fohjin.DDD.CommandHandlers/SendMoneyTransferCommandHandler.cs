using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public class SendMoneyTransferCommandHandler : CommandHandlerBase<SendMoneyTransferCommand>
    {
        private readonly IDomainRepository<IDomainEvent> _repository;

        public SendMoneyTransferCommandHandler(IDomainRepository<IDomainEvent> repository)
        {
            _repository = repository;
        }

        public override Task ExecuteAsync(SendMoneyTransferCommand compensatingCommand)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(compensatingCommand.Id);

            activeAccount?.SendTransferTo(new AccountNumber(compensatingCommand.AccountNumber), new Amount(compensatingCommand.Amount));

            return Task.CompletedTask;
        }
    }
}