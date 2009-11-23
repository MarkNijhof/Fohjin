using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.Events.Account;
using Fohjin.DDD.EventStore;

namespace Test.Fohjin.DDD.Scenarios.Depositing_cash
{
    public class When_depositing_cash : CommandTestFixture<DepositeCashCommand, DepositeCashCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountOpenedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new CashDepositedEvent(10, 10)).ToVersion(2);
        }

        protected override DepositeCashCommand When()
        {
            return new DepositeCashCommand(Guid.NewGuid(), 20);
        }

        [Then]
        public void Then_a_cash_deposited_event_will_be_published()
        {
            PublishedEvents.Last().WillBeOfType<CashDepositedEvent>();
        }

        [Then]
        public void Then_the_published_event_will_contain_the_amount_and_new_account_balance()
        {
            PublishedEvents.Last<CashDepositedEvent>().Balance.WillBe(30);
            PublishedEvents.Last<CashDepositedEvent>().Amount.WillBe(20);
        }
    }
}