using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public class WithdrawlCashCommandHandler : ICommandHandler<WithdrawlCashCommand>
    {
        private readonly IDomainRepository _repository;

        public WithdrawlCashCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(WithdrawlCashCommand compensatingCommand)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(compensatingCommand.Id);

            activeAccount.Withdrawl(new Amount(compensatingCommand.Amount));

            _repository.Save(activeAccount);
        }
    }
}