using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.Events;
using Fohjin.DDD.Events.Account;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Client_wants_to_close_an_account
{
    public class When_closing_an_account : CommandTestFixture<CloseAccountCommand, CloseAccountCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountOpenedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
        }

        protected override CloseAccountCommand When()
        {
            return new CloseAccountCommand(Guid.NewGuid());
        }

        [Then]
        public void Then_an_account_closed_event_will_be_published()
        {
            PublishedEvents.Last().WillBeOfType<AccountClosedEvent>();
        }

        [Then]
        public void Then_the_newly_created_closed_account_will_be_saved()
        {
            OnDependency<IDomainRepository>().Verify(x => x.Save(It.IsAny<ClosedAccount>()));
        }
    }
}