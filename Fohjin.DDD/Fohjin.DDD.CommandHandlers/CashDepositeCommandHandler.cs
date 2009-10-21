using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.CommandHandlers
{
    public class CashDepositeCommandHandler : ICommandHandler<CashDepositeCommand>
    {
        private readonly IDomainRepository _repository;

        public CashDepositeCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(CashDepositeCommand command)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(command.Id);

            activeAccount.Deposite(new Amount(command.Amount));

            _repository.Save(activeAccount);
        }
    }
}