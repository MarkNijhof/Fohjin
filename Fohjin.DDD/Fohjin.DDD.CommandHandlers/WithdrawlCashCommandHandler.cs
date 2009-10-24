using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.CommandHandlers
{
    public class WithdrawlCashCommandHandler : ICommandHandler<WithdrawlCashCommand>
    {
        private readonly IDomainRepository _repository;

        public WithdrawlCashCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(WithdrawlCashCommand command)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(command.Id);

            activeAccount.Withdrawl(new Amount(command.Amount));

            _repository.Save(activeAccount);
        }
    }
}