using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public class WithdrawalCashCommandHandler : ICommandHandler<WithdrawCashCommand>
    {
        private readonly IDomainRepository<IDomainEvent> _repository;

        public WithdrawalCashCommandHandler(IDomainRepository<IDomainEvent> repository)
        {
            _repository = repository;
        }

        public void Execute(WithdrawCashCommand compensatingCommand)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(compensatingCommand.Id);

            activeAccount.Withdrawl(new Amount(compensatingCommand.Amount));
        }
    }
}