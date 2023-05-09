using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public class WithdrawlCashCommandHandler : ICommandHandler<WithdrawCashCommand>
    {
        private readonly IDomainRepository<IDomainEvent> _repository;

        public WithdrawlCashCommandHandler(IDomainRepository<IDomainEvent> repository)
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