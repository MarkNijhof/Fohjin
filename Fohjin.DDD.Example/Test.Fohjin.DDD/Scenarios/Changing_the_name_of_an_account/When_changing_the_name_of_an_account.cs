using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.Events.Account;
using Fohjin.DDD.EventStore;

namespace Test.Fohjin.DDD.Scenarios.Changing_the_name_of_an_account
{
    public class When_changing_the_name_of_an_account : CommandTestFixture<ChangeAccountNameCommand, ChangeAccountNameCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountOpenedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
        }

        protected override ChangeAccountNameCommand When()
        {
            return new ChangeAccountNameCommand(Guid.NewGuid(), "New Account Name");
        }

        [Then]
        public void Then_an_account_name_changed_event_will_be_published()
        {
            PublishedEvents.Last().WillBeOfType<AccountNameChangedEvent>();
        }

        [Then]
        public void Then_the_published_event_will_contain_the_new_name_of_the_account()
        {
            PublishedEvents.Last<AccountNameChangedEvent>().AccountName.WillBe("New Account Name");
        }
    }
}