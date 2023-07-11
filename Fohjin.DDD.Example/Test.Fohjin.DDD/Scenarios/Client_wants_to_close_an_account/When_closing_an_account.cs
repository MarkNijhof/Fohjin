using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.Events.Account;
using Fohjin.DDD.EventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Client_wants_to_close_an_account;

public class When_closing_an_account : CommandTestFixture<CloseAccountCommand, CloseAccountCommandHandler, ActiveAccount>
{
    protected override IEnumerable<IDomainEvent> Given()
    {
        yield return PrepareDomainEvent.Set(new AccountOpenedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
    }

    protected override CloseAccountCommand When() => new(Guid.NewGuid());

    [TestMethod]
    public void Then_an_account_closed_event_will_be_published()
    {
        PublishedEvents?.Last().WillBeOfType<AccountClosedEvent>();
    }

    [TestMethod]
    public void Then_the_newly_created_closed_account_will_be_saved()
    {
        OnDependency<IDomainRepository<IDomainEvent>>().Verify(x => x.Add(It.IsAny<ClosedAccount>()));
    }
}