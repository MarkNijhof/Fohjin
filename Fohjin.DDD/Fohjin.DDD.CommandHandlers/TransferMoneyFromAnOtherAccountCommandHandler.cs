using System;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.EventStorage;

namespace Fohjin.DDD.CommandHandlers
{
    public class TransferMoneyFromAnOtherAccountCommandHandler : ICommandHandler<TransferMoneyFromAnOtherAccountCommand>
    {
        private readonly IDomainRepository _repository;

        public TransferMoneyFromAnOtherAccountCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(TransferMoneyFromAnOtherAccountCommand command)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(command.Id);

            activeAccount.ReceiveTransferFrom(new AccountNumber(command.AccountNumber), new Amount(command.Amount));

            _repository.Save(activeAccount);
        }
    }
}