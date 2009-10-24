using System;
using System.Linq;
using System.Collections.Generic;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.Exceptions;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events;
using Fohjin.DDD.Events.ActiveAccount;
using Fohjin.DDD.Reporting.Dto;
using Moq;

namespace Test.Fohjin.DDD.Scenarios
{
    public class When_changing_the_name_of_an_account : CommandTestFixture<ChangeAccountNameCommand, ChangeAccountNameCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
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

    public class When_changing_the_name_of_an_account_that_is_not_yet_created : CommandTestFixture<ChangeAccountNameCommand, ChangeAccountNameCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override ChangeAccountNameCommand When()
        {
            return new ChangeAccountNameCommand(Guid.NewGuid(), "New Account Name");
        }

        [Then]
        public void Then_an_account_was_not_created_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<AccountWasNotCreatedException>();
        }

        [Then]
        public void Then_the_exception_message_will_be()
        {
            CaughtException.Message.WillBe("The ActiveAcount is not created and no opperations can be executed on it");
        }
    }

    public class When_changing_the_name_of_an_account_that_is_closed : CommandTestFixture<ChangeAccountNameCommand, ChangeAccountNameCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new AccountClosedEvent()).ToVersion(2);
        }

        protected override ChangeAccountNameCommand When()
        {
            return new ChangeAccountNameCommand(Guid.NewGuid(), "New Account Name");
        }

        [Then]
        public void Then_an_account_was_closed_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<AccountWasClosedException>();
        }

        [Then]
        public void Then_the_exception_message_will_be()
        {
            CaughtException.Message.WillBe("The ActiveAcount is closed and no opperations can be executed on it");
        }
    }

    public class When_an_account_name_was_changed : EventTestFixture<AccountNameChangedEvent, AccountNameChangedEventHandler>
    {
        private static Guid _accountId;
        private object UpdateAccountObject;
        private object WhereAccountObject;
        private object UpdateAccountDetailsObject;
        private object WhereAccountDetailsObject;

        protected override void MockSetup()
        {
            GetMock<IReportingRepository>()
                .Setup(x => x.Update<AccountReport>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { UpdateAccountObject = u; WhereAccountObject = w; });

            GetMock<IReportingRepository>()
                .Setup(x => x.Update<AccountDetailsReport>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { UpdateAccountDetailsObject = u; WhereAccountDetailsObject = w; });
        }

        protected override AccountNameChangedEvent When()
        {
            var accountNameGotChangedEvent = new AccountNameChangedEvent("New Account Name") { AggregateId = Guid.NewGuid() };
            _accountId = accountNameGotChangedEvent.AggregateId;
            return accountNameGotChangedEvent;
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_update_the_client_report()
        {
            GetMock<IReportingRepository>().Verify(x => x.Update<AccountReport>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [Then]
        public void Then_the_account_report_will_be_updated_with_the_expected_details()
        {
            UpdateAccountObject.WillBeSimuliar(new { Name = "New Account Name" }.ToString());
            WhereAccountObject.WillBeSimuliar(new { Id = _accountId });
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_update_the_client_details_report()
        {
            GetMock<IReportingRepository>().Verify(x => x.Update<AccountDetailsReport>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [Then]
        public void Then_the_account_details_report_will_be_updated_with_the_expected_details()
        {
            UpdateAccountDetailsObject.WillBeSimuliar(new { AccountName = "New Account Name" }.ToString());
            WhereAccountDetailsObject.WillBeSimuliar(new { Id = _accountId });
        }
    }
}