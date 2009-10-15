using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.EventStorage;

namespace Fohjin.DDD.CommandHandlers
{
    public class CashWithdrawlCommandHandler : ICommandHandler<CashWithdrawlCommand>
    {
        private readonly IRepository _repository;

        public CashWithdrawlCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Execute(CashWithdrawlCommand command)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(command.Id);

            activeAccount.Withdrawl(new Amount(command.Amount));

            _repository.Save(activeAccount);
        }
    }
}