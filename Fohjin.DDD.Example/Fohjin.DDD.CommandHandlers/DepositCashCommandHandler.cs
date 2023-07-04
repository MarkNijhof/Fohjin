using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public class DepositCashCommandHandler : CommandHandlerBase<DepositCashCommand>
    {
        private readonly IDomainRepository<IDomainEvent> _repository;

        public DepositCashCommandHandler(IDomainRepository<IDomainEvent> repository)
        {
            _repository = repository;
        }

        public override Task ExecuteAsync(DepositCashCommand compensatingCommand)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(compensatingCommand.Id);

            activeAccount?.Deposit(new Amount(compensatingCommand.Amount));

            return Task.CompletedTask;
        }
    }
}