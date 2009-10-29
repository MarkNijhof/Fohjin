using System;
using System.Linq;
using System.Collections.Generic;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.Exceptions;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events;
using Fohjin.DDD.Events.ActiveAccount;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting.Dto;
using Moq;

namespace Test.Fohjin.DDD.Scenarios
{
    public class When_adding_a_new_account_to_a_client : CommandTestFixture<AddNewAccountToClientCommand, AddNewAccountToClientCommandHandler, Client>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new ClientCreatedEvent(Guid.NewGuid(), "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937")).ToVersion(1);
        }

        protected override AddNewAccountToClientCommand When()
        {
            return new AddNewAccountToClientCommand(Guid.NewGuid(), "New Account");
        }

        [Then]
        public void Then_an_account_to_client_assigned_event_will_be_published()
        {
            PublishedEvents.Last().WillBeOfType<AccountToClientAssignedEvent>();
        }

        [Then]
        public void Then_the_published_event_will_contain_the_expected_details_of_the_account()
        {
            PublishedEvents.Last<AccountToClientAssignedEvent>().AggregateId.WillBe(AggregateRoot.Id);
            PublishedEvents.Last<AccountToClientAssignedEvent>().AccountId.WillNotBe(new Guid());
        }

        [Then]
        public void Then_the_newly_created_account_will_be_saved()
        {
            OnDependency<IDomainRepository>().Verify(x => x.Save(It.IsAny<ActiveAccount>()));
        }
    }

    public class When_adding_a_new_account_to_a_not_yet_created_client : CommandTestFixture<AddNewAccountToClientCommand, AddNewAccountToClientCommandHandler, Client>
    {
        protected override AddNewAccountToClientCommand When()
        {
            return new AddNewAccountToClientCommand(Guid.NewGuid(), "New Account");
        }

        [Then]
        public void Then_a_client_was_not_created_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<ClientWasNotCreatedException>();
        }

        [Then]
        public void Then_the_exception_message_will_be()
        {
            CaughtException.Message.WillBe("The Client is not created and no opperations can be executed on it");
        }

        [Then]
        public void Then_there_is_no_new_account_to_be_saved()
        {
            OnDependency<IDomainRepository>().Verify(x => x.Save(It.IsAny<ActiveAccount>()), Times.Never());
        }
    }

    public class When_creating_a_new_account : AggregateRootTestFixture<ActiveAccount>
    {
        private string _ticks;

        protected override IEnumerable<IDomainEvent> Given()
        {
            SystemDateTime.Now = () => new DateTime(2009, 1, 1, 1, 1, 1, 1);
            return new List<IDomainEvent>();
        }

        protected override void When()
        {
            _ticks = SystemDateTime.Now().Ticks.ToString();
            AggregateRoot = ActiveAccount.CreateNew(Guid.NewGuid(), "New Account");
        }

        [Then]
        public void Then_an_account_created_event_will_be_published()
        {
            PublishedEvents.Last().WillBeOfType<AccountCreatedEvent>();
        }

        [Then]
        public void Then_the_published_event_will_contain_the_new_name_and_number_of_the_account()
        {
            PublishedEvents.Last<AccountCreatedEvent>().AccountName.WillBe("New Account");
            PublishedEvents.Last<AccountCreatedEvent>().AccountNumber.WillBe(_ticks);
        }

        [Then]
        public void Then_the_published_event_will_have_the_same_aggregate_id()
        {
            PublishedEvents.Last<AccountCreatedEvent>().AccountId.WillBe(AggregateRoot.Id);
        }

        protected override void Finally()
        {
            SystemDateTime.Reset();
        }
    }

    public class When_an_account_was_assigned_to_a_client : EventTestFixture<AccountToClientAssignedEvent, AccountToClientAssignedEventHandler>
    {
        protected override AccountToClientAssignedEvent When()
        {
            return new AccountToClientAssignedEvent(Guid.NewGuid()) { AggregateId = Guid.NewGuid() };
        }

        [Then]
        public void Then_it_will_not_throw_an_exception()
        {
            CaughtException.WillBeOfType<ThereWasNoExceptionButOneWasExpectedException>();
        }
    }

    public class When_an_account_was_created : EventTestFixture<AccountCreatedEvent, AccountCreatedEventHandler>
    {
        private static Guid _clientId;
        private static Guid _accountId;
        private AccountReport SaveAccountReportObject;
        private AccountDetailsReport SaveAccountDetailsReportObject;

        protected override void SetupDependencies()
        {
            OnDependency<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<AccountReport>()))
                .Callback<AccountReport>(a => SaveAccountReportObject = a);

            OnDependency<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<AccountDetailsReport>()))
                .Callback<AccountDetailsReport>(a => SaveAccountDetailsReportObject = a);
        }

        protected override AccountCreatedEvent When()
        {
            _accountId = Guid.NewGuid();
            _clientId = Guid.NewGuid();
            var accountCreatedEvent = new AccountCreatedEvent(_accountId, _clientId, "New Account", "1234567890");
            return accountCreatedEvent;
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_update_the_account_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Save(It.IsAny<AccountReport>()));
        }

        [Then]
        public void Then_the_account_report_will_be_updated_with_the_expected_details()
        {
            SaveAccountReportObject.Id.WillBe(_accountId);
            SaveAccountReportObject.ClientDetailsReportId.WillBe(_clientId);
            SaveAccountReportObject.AccountName.WillBe("New Account");
            SaveAccountReportObject.AccountNumber.WillBe("1234567890");
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_update_the_account_details_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Save(It.IsAny<AccountDetailsReport>()));
        }

        [Then]
        public void Then_the_account_details_report_will_be_updated_with_the_expected_details()
        {
            SaveAccountDetailsReportObject.Id.WillBe(_accountId);
            SaveAccountDetailsReportObject.ClientReportId.WillBe(_clientId);
            SaveAccountDetailsReportObject.AccountName.WillBe("New Account");
            SaveAccountDetailsReportObject.AccountNumber.WillBe("1234567890");
            SaveAccountDetailsReportObject.Balance.WillBe(0.0M);
        }
    }
}