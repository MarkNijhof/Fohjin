using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.EventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Scenarios.Assign_new_bank_card;

public class When_assigning_a_new_bank_card : CommandTestFixture<AssignNewBankCardCommand, AssignNewBankCardCommandHandler, Client>
{
    private readonly Guid _accountId = Guid.NewGuid();
    private readonly Guid _clientId = Guid.NewGuid();

    protected override IEnumerable<IDomainEvent> Given()
    {
        yield return PrepareDomainEvent.Set(new ClientCreatedEvent(_clientId, "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937")).ToVersion(1);
        yield return PrepareDomainEvent.Set(new AccountToClientAssignedEvent(_accountId)).ToVersion(2);
    }

    protected override AssignNewBankCardCommand When() => new (_clientId, _accountId);

    [TestMethod]
    public void Then_a_client_created_event_will_be_published()
    {
        PublishedEvents?.Last().WillBeOfType<NewBankCardForAccountAsignedEvent>();
    }

    [TestMethod]
    public void Then_the_published_event_will_contain_the_name_of_the_client()
    {
        PublishedEvents?.Last<NewBankCardForAccountAsignedEvent>()?.AccountId.WillBe(_accountId);
    }
}