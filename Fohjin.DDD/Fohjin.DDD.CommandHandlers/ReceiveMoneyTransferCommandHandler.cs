using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public class ReceiveMoneyTransferCommandHandler : ICommandHandler<ReceiveMoneyTransferCommand>
    {
        private readonly IDomainRepository _domainRepository;

        public ReceiveMoneyTransferCommandHandler(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public void Execute(ReceiveMoneyTransferCommand compensatingCommand)
        {
            var activeAccount = _domainRepository.GetById<ActiveAccount>(compensatingCommand.Id);

            activeAccount.ReceiveTransferFrom(new AccountNumber(compensatingCommand.AccountNumber), new Amount(compensatingCommand.Amount));

            _domainRepository.Save(activeAccount);
        }
    }
}