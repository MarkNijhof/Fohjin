using System;
using System.Linq;
using System.Collections.Generic;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Bus;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.Exceptions;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events;
using Fohjin.DDD.Events.ActiveAccount;
using Fohjin.DDD.Events.ClosedAccount;
using Fohjin.DDD.Reporting.Dto;
using Moq;

namespace Test.Fohjin.DDD.Scenarios
{
    public class When_clicking_to_closing_an_account : PresenterTestFixture<AccountDetailsPresenter>
    {
        protected override void SetupDependencies()
        {
            OnDependency<IPopupPresenter>()
                .Setup(x => x.CatchPossibleException(It.IsAny<Action>()))
                .Callback<Action>(x => x());

            var accountDetailsReports = new List<AccountDetailsReport> { new AccountDetailsReport(Guid.NewGuid(), Guid.NewGuid(), "Account name", 10.5M, "1234567890") };
            OnDependency<IReportingRepository>()
                .Setup(x => x.GetByExample<AccountDetailsReport>(It.IsAny<object>()))
                .Returns(accountDetailsReports);

            var accountReports = new List<AccountReport> { new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "Account name 1", "1234567890") };
            OnDependency<IReportingRepository>()
                .Setup(x => x.GetByExample<AccountReport>(It.IsAny<object>()))
                .Returns(accountReports);
        }

        protected override void Given()
        {
            Presenter.SetAccount(new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "Account name", "1234567890"));
            Presenter.Display();
        }

        protected override void When()
        {
            On<IAccountDetailsView>().FireEvent(x => x.OnCloseTheAccount += null);
        }

        [Then]
        public void Then_a_close_account_command_gets_send_to_the_bus()
        {
            On<ICommandBus>().VerifyThat.Method(x => x.Publish(It.IsAny<CloseAccountCommand>())).WasCalled();
        }

        [Then]
        public void Then_the_view_will_be_closed()
        {
            On<IAccountDetailsView>().VerifyThat.Method(x => x.Close()).WasCalled();
        }
    }

    public class When_closing_an_account : CommandTestFixture<CloseAccountCommand, CloseAccountCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
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

    public class When_creating_a_closed_account : AggregateRootTestFixture<ClosedAccount>
    {
        private List<Ledger> ledgers;
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

        [Then]
        public void Then_a_closed_account_created_event_will_be_published()
        {
            PublishedEvents.Last().WillBeOfType<ClosedAccountCreatedEvent>();
        }

        [Then]
        public void Then_the_published_event_will_contain_the_expected_details_of_the_closed_account()
        {
            PublishedEvents.Last<ClosedAccountCreatedEvent>().OriginalAccountId.WillBe(_accountId);
            PublishedEvents.Last<ClosedAccountCreatedEvent>().ClientId.WillBe(_clientId);
            PublishedEvents.Last<ClosedAccountCreatedEvent>().AccountName.WillBe("Closed Account");
            PublishedEvents.Last<ClosedAccountCreatedEvent>().AccountNumber.WillBe("1234567890");
        }

        [Then]
        public void Then_the_published_event_will_contain_the_expected_ledgers_of_the_closed_account()
        {
            PublishedEvents.Last<ClosedAccountCreatedEvent>().Ledgers.Count().WillBe(4);
            PublishedEvents.Last<ClosedAccountCreatedEvent>().Ledgers[0].Key.WillBe("CreditMutation");
            PublishedEvents.Last<ClosedAccountCreatedEvent>().Ledgers[1].Key.WillBe("DebitMutation");
            PublishedEvents.Last<ClosedAccountCreatedEvent>().Ledgers[2].Key.WillBe("CreditTransfer");
            PublishedEvents.Last<ClosedAccountCreatedEvent>().Ledgers[3].Key.WillBe("DebitTransfer");
        }
    }

    public class When_closing_a_not_yet_created_account : CommandTestFixture<CloseAccountCommand, CloseAccountCommandHandler, ActiveAccount>
    {
        protected override CloseAccountCommand When()
        {
            return new CloseAccountCommand(Guid.NewGuid());
        }

        [Then]
        public void Then_an_account_was_not_created_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<AccountWasNotCreatedException>();
        }
    }

    public class When_closing_a_closed_account : CommandTestFixture<CloseAccountCommand, CloseAccountCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new AccountClosedEvent()).ToVersion(2);
        }

        protected override CloseAccountCommand When()
        {
            return new CloseAccountCommand(Guid.NewGuid());
        }

        [Then]
        public void Then_an_account_was_closed_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<AccountWasClosedException>();
        }
    }

    public class When_closing_an_account_with_a_positive_balance : CommandTestFixture<CloseAccountCommand, CloseAccountCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new CashDepositedEvent(20, 20)).ToVersion(1);
        }

        protected override CloseAccountCommand When()
        {
            return new CloseAccountCommand(Guid.NewGuid());
        }

        [Then]
        public void Then_an_account_must_first_be_emptied_before_closing_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<AccountMustFirstBeEmptiedBeforeClosingException>();
        }
    }

    public class When_an_account_was_closed : EventTestFixture<AccountClosedEvent, AccountClosedEventHandler>
    {
        protected override AccountClosedEvent When()
        {
            return new AccountClosedEvent { AggregateId = Guid.NewGuid() };
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_update_the_account_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Delete<AccountReport>(It.IsAny<object>()), Times.Once());
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_update_the_account_details_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Delete<AccountDetailsReport>(It.IsAny<object>()), Times.Once());
        }
    }

    public class When_an_closed_account_was_created : EventTestFixture<ClosedAccountCreatedEvent, ClosedAccountCreatedEventHandler>
    {
        private static Guid _orginalAccountId;
        private static Guid _clientId;
        private ClosedAccountReport SaveClosedAccountReportObject;
        private ClosedAccountDetailsReport SaveClosedAccountDetailsReportObject;
        private List<KeyValuePair<string, string>> ledgers;
        private Guid _accountId;

        protected override void SetupDependencies()
        {
            OnDependency<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<ClosedAccountReport>()))
                .Callback<ClosedAccountReport>(a => SaveClosedAccountReportObject = a);

            OnDependency<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<ClosedAccountDetailsReport>()))
                .Callback<ClosedAccountDetailsReport>(a => SaveClosedAccountDetailsReportObject = a);
        }

        protected override ClosedAccountCreatedEvent When()
        {
            _accountId = Guid.NewGuid();
            _orginalAccountId = Guid.NewGuid();
            _clientId = Guid.NewGuid();

            ledgers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("CreditMutation" , "10.5|"),
                new KeyValuePair<string, string>("DebitMutation" , "15.0|"),
                new KeyValuePair<string, string>("CreditTransfer" , "10.5|1234567890"),
                new KeyValuePair<string, string>("DebitTransfer" , "15.0|0987654321"),
                new KeyValuePair<string, string>("CreditTransferFailed" , "15.0|0987654321"),
            };

            return new ClosedAccountCreatedEvent(_accountId, _orginalAccountId, _clientId, ledgers, "Closed Account", "1234567890");
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_save_the_closed_account_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Save(It.IsAny<ClosedAccountReport>()));
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_update_the_closed_account_report()
        {
            SaveClosedAccountReportObject.Id.WillBe(_accountId);
            SaveClosedAccountReportObject.ClientDetailsReportId.WillBe(_clientId);
            SaveClosedAccountReportObject.AccountName.WillBe("Closed Account");
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_save_the_closed_account_details_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Save(It.IsAny<ClosedAccountDetailsReport>()));
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_update_the_closed_account_details_report()
        {
            SaveClosedAccountDetailsReportObject.Id.WillBe(_accountId);
            SaveClosedAccountDetailsReportObject.ClientReportId.WillBe(_clientId);
            SaveClosedAccountDetailsReportObject.Balance.WillBe(0);
            SaveClosedAccountDetailsReportObject.AccountName.WillBe("Closed Account");
            SaveClosedAccountDetailsReportObject.AccountNumber.WillBe("1234567890");
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_save_the_four_ledger_reports()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Save(It.IsAny<LedgerReport>()), Times.Exactly(5));
        }
    }
}