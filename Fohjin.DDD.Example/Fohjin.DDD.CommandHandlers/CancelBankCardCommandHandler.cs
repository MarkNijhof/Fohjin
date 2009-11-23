using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public class CancelBankCardCommandHandler : ICommandHandler<CancelBankCardCommand>
    {
        private readonly IDomainRepository<IDomainEvent> _repository;

        public CancelBankCardCommandHandler(IDomainRepository<IDomainEvent> repository)
        {
            _repository = repository;
        }

        public void Execute(CancelBankCardCommand cancelReportStolenBankCardCommand)
        {
            var client = _repository.GetById<Client>(cancelReportStolenBankCardCommand.Id);
            var bankCard = client.GetBankCard(cancelReportStolenBankCardCommand.BankCardId);
            bankCard.ClientCancelsBankCard();
            _repository.Add(client);
        }
    }
}