using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.EventStorage;

namespace Fohjin.DDD.CommandHandlers
{
    public class TransferMoneyToAnOtherAccountCommandHandler : ICommandHandler<TransferMoneyToAnOtherAccountCommand>
    {
        private readonly IRepository _repository;

        public TransferMoneyToAnOtherAccountCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Execute(TransferMoneyToAnOtherAccountCommand command)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(command.Id);

            activeAccount.SendTransferTo(new AccountNumber(command.AccountNumber), new Amount(command.Amount));

            _repository.Save(activeAccount);
        }
    }
}