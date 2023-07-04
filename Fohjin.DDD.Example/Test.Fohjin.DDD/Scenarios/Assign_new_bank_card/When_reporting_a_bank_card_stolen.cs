using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.EventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Scenarios.Assign_new_bank_card;

public class When_reporting_a_bank_card_stolen : CommandTestFixture<ReportStolenBankCardCommand, ReportStolenBankCardCommandHandler, Client>
{
    private readonly Guid _bankCardId = Guid.NewGuid();
    private readonly Guid _accountId = Guid.NewGuid();
    private readonly Guid _clientId = Guid.NewGuid();

    protected override IEnumerable<IDomainEvent> Given()
    {
        yield return PrepareDomainEvent.Set(new ClientCreatedEvent(_clientId, "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937")).ToVersion(1);
        yield return PrepareDomainEvent.Set(new AccountToClientAssignedEvent(_accountId)).ToVersion(2);
        yield return PrepareDomainEvent.Set(new NewBankCardForAccountAsignedEvent(_bankCardId, _accountId)).ToVersion(3);
    }

    protected override ReportStolenBankCardCommand When() => new(_clientId, _bankCardId);

    [TestMethod]
    public void Then_a_client_created_event_will_be_published()
    {
        PublishedEvents?.Last().WillBeOfType<BankCardWasReportedStolenEvent>();
    }
}