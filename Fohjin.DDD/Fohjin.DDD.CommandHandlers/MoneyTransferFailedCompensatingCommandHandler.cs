using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public class MoneyTransferFailedCompensatingCommandHandler : ICommandHandler<MoneyTransferFailedCompensatingCommand>
    {
        private readonly IDomainRepository _repository;

        public MoneyTransferFailedCompensatingCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(MoneyTransferFailedCompensatingCommand compensatingCommand)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(compensatingCommand.Id);

            activeAccount.PreviousTransferFailed(new AccountNumber(compensatingCommand.AccountNumber), new Amount(compensatingCommand.Amount));

            //_repository.Add(activeAccount);
            _repository.Complete();
        }
    }
}