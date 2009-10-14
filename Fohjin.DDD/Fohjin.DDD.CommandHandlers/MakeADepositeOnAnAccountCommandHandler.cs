using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.EventStorage;

namespace Fohjin.DDD.CommandHandlers
{
    public class MakeADepositeOnAnAccountCommandHandler : ICommandHandler<CashDepositeCommand>
    {
        private readonly IRepository<ActiveAccount> _repository;

        public MakeADepositeOnAnAccountCommandHandler(IRepository<ActiveAccount> repository)
        {
            _repository = repository;
        }

        public void Execute(CashDepositeCommand command)
        {
            var activeAccount = _repository.GetById(command.Id);

            activeAccount.Deposite(new Amount(command.Amount));

            _repository.Save(activeAccount);
        }
    }
}