using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.CommandHandlers
{
    public class SendMoneyTransferCommandHandler : ICommandHandler<SendMoneyTransferCommand>
    {
        private readonly IDomainRepository _repository;

        public SendMoneyTransferCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(SendMoneyTransferCommand command)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(command.Id);

            activeAccount.SendTransferTo(new AccountNumber(command.AccountNumber), new Amount(command.Amount));

            _repository.Save(activeAccount);
        }
    }
}