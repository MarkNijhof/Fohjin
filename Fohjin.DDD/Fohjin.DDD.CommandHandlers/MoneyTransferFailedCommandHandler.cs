using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.CommandHandlers
{
    public class MoneyTransferFailedCommandHandler : ICommandHandler<MoneyTransferFailedCommand>
    {
        private readonly IDomainRepository _repository;

        public MoneyTransferFailedCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(MoneyTransferFailedCommand command)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(command.Id);

            activeAccount.PreviousTransferFailed(new AccountNumber(command.AccountNumber), new Amount(command.Amount));

            _repository.Save(activeAccount);
        }
    }
}