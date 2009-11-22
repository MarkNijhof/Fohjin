using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public class DepositeCashCommandHandler : ICommandHandler<DepositeCashCommand>
    {
        private readonly IDomainRepository<IDomainEvent> _repository;

        public DepositeCashCommandHandler(IDomainRepository<IDomainEvent> repository)
        {
            _repository = repository;
        }

        public void Execute(DepositeCashCommand compensatingCommand)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(compensatingCommand.Id);

            activeAccount.Deposite(new Amount(compensatingCommand.Amount));
        }
    }
}