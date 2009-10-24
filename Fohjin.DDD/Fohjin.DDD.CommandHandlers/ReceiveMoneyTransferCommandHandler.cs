using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.CommandHandlers
{
    public class ReceiveMoneyTransferCommandHandler : ICommandHandler<ReceiveMoneyTransferCommand>
    {
        private readonly IDomainRepository _domainRepository;

        public ReceiveMoneyTransferCommandHandler(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public void Execute(ReceiveMoneyTransferCommand command)
        {
            var activeAccount = _domainRepository.GetById<ActiveAccount>(command.Id);

            activeAccount.ReceiveTransferFrom(new AccountNumber(command.AccountNumber), new Amount(command.Amount));

            _domainRepository.Save(activeAccount);
        }
    }
}