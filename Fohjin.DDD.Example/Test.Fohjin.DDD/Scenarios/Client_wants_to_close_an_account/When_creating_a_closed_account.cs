using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.Events.Account;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Scenarios.Client_wants_to_close_an_account;

public class When_creating_a_closed_account : AggregateRootTestFixture<ClosedAccount>
{
    private List<Ledger>? ledgers;
    private Guid _accountId;
    private Guid _clientId;

    protected override void When()
    {
        ledgers = new List<Ledger>
            {
                new CreditMutation(10.5M, new AccountNumber(string.Empty)), 
                new DebitMutation(15.0M, new AccountNumber(string.Empty)),
                new CreditTransfer(10.5M, new AccountNumber("1234567890")), 
                new DebitTransfer(15.0M, new AccountNumber("0987654321")),
            };

        _accountId = Guid.NewGuid();
        _clientId = Guid.NewGuid();
        AggregateRoot = ClosedAccount.CreateNew(_accountId, _clientId, ledgers, new AccountName("Closed Account"), new AccountNumber("1234567890"));
    }

    [TestMethod]
    public void Then_a_closed_account_created_event_will_be_published()
    {
        PublishedEvents?.Last().WillBeOfType<ClosedAccountCreatedEvent>();
    }

    [TestMethod]
    public void Then_the_published_event_will_contain_the_expected_details_of_the_closed_account()
    {
        PublishedEvents?.Last<ClosedAccountCreatedEvent>().OriginalAccountId.WillBe(_accountId);
        PublishedEvents?.Last<ClosedAccountCreatedEvent>().ClientId.WillBe(_clientId);
        PublishedEvents?.Last<ClosedAccountCreatedEvent>().AccountName.WillBe("Closed Account");
        PublishedEvents?.Last<ClosedAccountCreatedEvent>().AccountNumber.WillBe("1234567890");
    }

    [TestMethod]
    public void Then_the_published_event_will_contain_the_expected_ledgers_of_the_closed_account()
    {
        PublishedEvents?.Last<ClosedAccountCreatedEvent>().Ledgers.Count().WillBe(4);
        PublishedEvents?.Last<ClosedAccountCreatedEvent>().Ledgers[0].Key.WillBe("CreditMutation");
        PublishedEvents?.Last<ClosedAccountCreatedEvent>().Ledgers[1].Key.WillBe("DebitMutation");
        PublishedEvents?.Last<ClosedAccountCreatedEvent>().Ledgers[2].Key.WillBe("CreditTransfer");
        PublishedEvents?.Last<ClosedAccountCreatedEvent>().Ledgers[3].Key.WillBe("DebitTransfer");
    }
}