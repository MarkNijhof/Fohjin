using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers;

public class CancelBankCardCommandHandler : CommandHandlerBase<CancelBankCardCommand>
{
    private readonly IDomainRepository<IDomainEvent> _repository;

    public CancelBankCardCommandHandler(IDomainRepository<IDomainEvent> repository)
    {
        _repository = repository;
    }

    public override Task ExecuteAsync(CancelBankCardCommand cancelReportStolenBankCardCommand)
    {
        var client = _repository.GetById<Client>(cancelReportStolenBankCardCommand.Id);
        var bankCard = client?.GetBankCard(cancelReportStolenBankCardCommand.BankCardId);
        bankCard?.ClientCancelsBankCard();
        if (client != null)
            _repository.Add(client);
        return Task.CompletedTask;
    }
}