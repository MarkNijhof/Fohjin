using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public class ReportStolenBankCardCommandHandler : ICommandHandler<ReportStolenBankCardCommand>
    {
        private readonly IDomainRepository<IDomainEvent> _repository;

        public ReportStolenBankCardCommandHandler(IDomainRepository<IDomainEvent> repository)
        {
            _repository = repository;
        }

        public void Execute(ReportStolenBankCardCommand cancelReportStolenBankCardCommand)
        {
            var client = _repository.GetById<Client>(cancelReportStolenBankCardCommand.Id);
            var bankCard = client.GetBankCard(cancelReportStolenBankCardCommand.BankCardId);
            bankCard.BankCardIsReportedStolen();
            _repository.Add(client);
        }
    }
}