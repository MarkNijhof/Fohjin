using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public class SendMoneyTransferCommandHandler : ICommandHandler<SendMoneyTransferCommand>
    {
        private readonly IDomainRepository _repository;

        public SendMoneyTransferCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(SendMoneyTransferCommand compensatingCommand)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(compensatingCommand.Id);

            activeAccount.SendTransferTo(new AccountNumber(compensatingCommand.AccountNumber), new Amount(compensatingCommand.Amount));

            //_repository.Add(activeAccount);
            _repository.Complete();
        }
    }
}