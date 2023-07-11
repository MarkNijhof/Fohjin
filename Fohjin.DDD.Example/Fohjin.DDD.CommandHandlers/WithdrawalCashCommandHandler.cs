using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public class WithdrawalCashCommandHandler : CommandHandlerBase<WithdrawalCashCommand>
    {
        private readonly IDomainRepository<IDomainEvent> _repository;

        public WithdrawalCashCommandHandler(IDomainRepository<IDomainEvent> repository)
        {
            _repository = repository;
        }

        public override Task ExecuteAsync(WithdrawalCashCommand compensatingCommand)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(compensatingCommand.Id);

            activeAccount?.Withdrawal(new Amount(compensatingCommand.Amount));
            return Task.CompletedTask;
        }
    }
}