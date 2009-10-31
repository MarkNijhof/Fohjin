using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Account;

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

            _repository.Save(activeAccount);
        }
    }
}