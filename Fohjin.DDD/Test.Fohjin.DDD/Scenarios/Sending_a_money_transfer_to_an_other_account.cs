using System;
using System.Linq;
using System.Collections.Generic;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Bus;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events;
using Fohjin.DDD.Events.Account;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Services;
using Moq;

namespace Test.Fohjin.DDD.Scenarios
{
    public class When_clicking_to_make_a_money_transfer : PresenterTestFixture<AccountDetailsPresenter>
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
            On<IAccountDetailsView>().FireEvent(x => x.OnInitiateMoneyTransfer += null);
        }

        [Then]
        public void Then_the_current_amount_is_set_to_zero()
        {
            On<IAccountDetailsView>().VerifyThat.ValueIsSetFor(x => x.TransferAmount = 0M);
        }

        [Then]
        public void Then_the_save_button_will_be_disabled()
        {
            On<IAccountDetailsView>().VerifyThat.Method(x => x.DisableMenuButtons()).WasCalled();
        }

        [Then]
        public void Then_the_transfer_panel_will_be_enabled()
        {
            On<IAccountDetailsView>().VerifyThat.Method(x => x.EnableTransferPanel()).WasCalled();
        }
    }

    public class When_inserting_the_transfer_ammount : PresenterTestFixture<AccountDetailsPresenter>
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
            On<IAccountDetailsView>().ValueFor(x => x.AccountName).IsSetTo("Account name");
            On<IAccountDetailsView>().ValueFor(x => x.WithdrawlAmount).IsSetTo(0M);
            On<IAccountDetailsView>().ValueFor(x => x.DepositeAmount).IsSetTo(0M);
            On<IAccountDetailsView>().ValueFor(x => x.TransferAmount).IsSetTo(0M);
            On<IAccountDetailsView>().FireEvent(x => x.OnInitiateMoneyTransfer += null);
        }

        protected override void When()
        {
            On<IAccountDetailsView>().ValueFor(x => x.TransferAmount).IsSetTo(12.3M);
            On<IAccountDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
        }

        [Then]
        public void Then_the_save_button_will_be_enabled()
        {
            On<IAccountDetailsView>().VerifyThat.Method(x => x.EnableSaveButton()).WasCalled();
        }
    }

    public class When_clearing_the_transfer_ammount : PresenterTestFixture<AccountDetailsPresenter>
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
            On<IAccountDetailsView>().ValueFor(x => x.AccountName).IsSetTo("Account name");
            On<IAccountDetailsView>().ValueFor(x => x.WithdrawlAmount).IsSetTo(0M);
            On<IAccountDetailsView>().ValueFor(x => x.DepositeAmount).IsSetTo(0M);
            On<IAccountDetailsView>().ValueFor(x => x.TransferAmount).IsSetTo(0M);
            On<IAccountDetailsView>().FireEvent(x => x.OnInitiateMoneyTransfer += null);
            On<IAccountDetailsView>().ValueFor(x => x.TransferAmount).IsSetTo(12.3M);
            On<IAccountDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
        }

        protected override void When()
        {
            On<IAccountDetailsView>().ValueFor(x => x.TransferAmount).IsSetTo(0M);
            On<IAccountDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
        }

        [Then]
        public void Then_the_save_button_will_be_enabled()
        {
            On<IAccountDetailsView>().VerifyThat.Method(x => x.DisableSaveButton()).WasCalled();
        }
    }

    public class When_executing_the_money_transfer : PresenterTestFixture<AccountDetailsPresenter>
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

            OnDependency<IAccountDetailsView>()
                .Setup(x => x.GetSelectedTransferAccount())
                .Returns(new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "Account name", "1234567890"));
        }

        protected override void Given()
        {
            Presenter.SetAccount(new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "Account name", "1234567890"));
            Presenter.Display();
            On<IAccountDetailsView>().ValueFor(x => x.AccountName).IsSetTo("Account name");
            On<IAccountDetailsView>().ValueFor(x => x.WithdrawlAmount).IsSetTo(0M);
            On<IAccountDetailsView>().ValueFor(x => x.DepositeAmount).IsSetTo(0M);
            On<IAccountDetailsView>().ValueFor(x => x.TransferAmount).IsSetTo(0M);
            On<IAccountDetailsView>().FireEvent(x => x.OnInitiateMoneyTransfer += null);
            On<IAccountDetailsView>().ValueFor(x => x.TransferAmount).IsSetTo(12.3M);
            On<IAccountDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
        }

        protected override void When()
        {
            On<IAccountDetailsView>().FireEvent(x => x.OnTransferMoney += null);
        }

        [Then]
        public void Then_a_change_account_name_command_will_be_published()
        {
            On<ICommandBus>().VerifyThat.Method(x => x.Publish(It.IsAny<SendMoneyTransferCommand>())).WasCalled();
        }

        [Then]
        public void Then_the_save_button_will_be_enabled()
        {
            On<IAccountDetailsView>().VerifyThat.Method(x => x.EnableMenuButtons()).WasCalled();
        }

        [Then]
        public void Then_the_details_panel_will_be_enabled()
        {
            On<IAccountDetailsView>().VerifyThat.Method(x => x.EnableDetailsPanel()).WasCalled();
        }
    }

    public class When_canceling_to_make_a_money_transfer : PresenterTestFixture<AccountDetailsPresenter>
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
            On<IAccountDetailsView>().FireEvent(x => x.OnInitiateMoneyWithdrawl += null);
        }

        protected override void When()
        {
            On<IAccountDetailsView>().FireEvent(x => x.OnCancel += null);
        }

        [Then]
        public void Then_the_save_button_will_be_disabled()
        {
            On<IAccountDetailsView>().VerifyThat.Method(x => x.DisableSaveButton()).WasCalled();
        }

        [Then]
        public void Then_the_menu_buttons_will_be_enabled()
        {
            On<IAccountDetailsView>().VerifyThat.Method(x => x.EnableMenuButtons()).WasCalled();
        }

        [Then]
        public void Then_the_details_panel_will_be_enabled()
        {
            On<IAccountDetailsView>().VerifyThat.Method(x => x.EnableDetailsPanel()).WasCalled();
        }
    }

    public class When_sending_a_money_transfer : CommandTestFixture<SendMoneyTransferCommand, SendMoneyTransferCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new CashDepositedEvent(20, 20)).ToVersion(1);
        }

        protected override SendMoneyTransferCommand When()
        {
            return new SendMoneyTransferCommand(Guid.NewGuid(), 5.0M, "1234567890");
        }

        [Then]
        public void Then_a_money_transfer_event_will_be_published()
        {
            PublishedEvents.Last().WillBeOfType<MoneyTransferSendEvent>();
        }

        [Then]
        public void Then_the_published_event_will_contain_the_amount_and_new_account_balance()
        {
            PublishedEvents.Last<MoneyTransferSendEvent>().Amount.WillBe(5.0M);
            PublishedEvents.Last<MoneyTransferSendEvent>().Balance.WillBe(15.0M);
            PublishedEvents.Last<MoneyTransferSendEvent>().TargetAccount.WillBe("1234567890");
        }
    }

    public class When_sending_a_money_transfer_on_an_account_that_is_not_yet_created : CommandTestFixture<SendMoneyTransferCommand, SendMoneyTransferCommandHandler, ActiveAccount>
    {
        protected override SendMoneyTransferCommand When()
        {
            return new SendMoneyTransferCommand(Guid.NewGuid(), 10.0M, "1234567890");
        }

        [Then]
        public void Then_a_non_existing_account_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<NonExitsingAccountException>();
        }

        [Then]
        public void Then_the_exception_message_will_be()
        {
            CaughtException.Message.WillBe("The ActiveAcount is not created and no opperations can be executed on it");
        }
    }

    public class When_sending_a_money_transfer_on_an_account_that_is_closed : CommandTestFixture<SendMoneyTransferCommand, SendMoneyTransferCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new AccountClosedEvent()).ToVersion(2);
        }

        protected override SendMoneyTransferCommand When()
        {
            return new SendMoneyTransferCommand(Guid.NewGuid(), 10.0M, "1234567890");
        }

        [Then]
        public void Then_a_closed_account_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<ClosedAccountException>();
        }

        [Then]
        public void Then_the_exception_message_will_be()
        {
            CaughtException.Message.WillBe("The ActiveAcount is closed and no opperations can be executed on it");
        }
    }

    public class When_receiveing_a_money_transfer_on_an_account_that_has_to_little_balance : CommandTestFixture<SendMoneyTransferCommand, SendMoneyTransferCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
        }

        protected override SendMoneyTransferCommand When()
        {
            return new SendMoneyTransferCommand(Guid.NewGuid(), 10.5M, "1234567890");
        }

        [Then]
        public void Then_an_account_balance_to_low_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<AccountBalanceToLowException>();
        }

        [Then]
        public void Then_the_exception_message_will_be()
        {
            CaughtException.WithMessage(string.Format("The amount {0:C} is larger than your current balance {1:C}", 10.5M, 0));
        }
    }

    public class When_money_transfer_was_send : EventTestFixture<MoneyTransferSendEvent, MoneyTransferSendEventHandler>
    {
        private static Guid _accountId;
        private object UpdateAccountDetailsObject;
        private object WhereAccountDetailsObject;
        private LedgerReport LedgerReportObject;

        protected override void SetupDependencies()
        {
            OnDependency<IReportingRepository>()
                .Setup(x => x.Update<AccountDetailsReport>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { UpdateAccountDetailsObject = u; WhereAccountDetailsObject = w; });

            OnDependency<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<LedgerReport>()))
                .Callback<LedgerReport>(l => { LedgerReportObject = l; });
        }

        protected override MoneyTransferSendEvent When()
        {
            _accountId = Guid.NewGuid();
            return new MoneyTransferSendEvent(50.5M, 10.5M, "0987654321", "1234567890") { AggregateId = _accountId };
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_update_the_account_details_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Update<AccountDetailsReport>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_update_the_account_details()
        {
            UpdateAccountDetailsObject.WillBeSimuliar(new { Balance = 50.5M }.ToString());
            WhereAccountDetailsObject.WillBeSimuliar(new { Id = _accountId });
        }

        [Then]
        public void Then_the_account_details_report_will_be_updated_with_the_expected_details()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Save(It.IsAny<LedgerReport>()));
        }

        [Then]
        public void Then_the_ledger_report_will_be_saved_with_the_expected_details()
        {
            LedgerReportObject.AccountDetailsReportId.WillBe(_accountId);
            LedgerReportObject.Amount.WillBe(10.5M);
            LedgerReportObject.Action.WillBe("Transfer to 1234567890");
        }
    }

    public class When_money_transfer_was_send_further : EventTestFixture<MoneyTransferSendEvent, SendMoneyTransferFurtherEventHandler>
    {
        private static Guid _accountId;
        private MoneyTransfer MoneyTransfer;

        protected override void SetupDependencies()
        {
            OnDependency<ISendMoneyTransfer>()
                .Setup(x => x.Send(It.IsAny<MoneyTransfer>()))
                .Callback<MoneyTransfer>(x => MoneyTransfer = x);
        }

        protected override MoneyTransferSendEvent When()
        {
            _accountId = Guid.NewGuid();
            return new MoneyTransferSendEvent(50.5M, 10.5M, "0987654321", "1234567890") { AggregateId = _accountId };
        }

        [Then]
        public void Then_the_money_transfer_will_be_send_through_the_money_transfer_service()
        {
            OnDependency<ISendMoneyTransfer>().Verify(x => x.Send(It.IsAny<MoneyTransfer>()));
        }

        [Then]
        public void Then_the_money_transfer_will_have_the_expected_details()
        {
            MoneyTransfer.Ammount.WillBe(10.5M);
            MoneyTransfer.SourceAccount.WillBe("0987654321");
            MoneyTransfer.TargetAccount.WillBe("1234567890");
        }
    }

    public class When_sending_a_money_transfer_failed : CommandTestFixture<MoneyTransferFailedCommand, MoneyTransferFailedCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new CashDepositedEvent(20, 20)).ToVersion(2);
            yield return PrepareDomainEvent.Set(new MoneyTransferSendEvent(15,5, "1234567890", "0987654321")).ToVersion(3);
        }

        protected override MoneyTransferFailedCommand When()
        {
            return new MoneyTransferFailedCommand(Guid.NewGuid(), 5.0M, "0987654321");
        }

        [Then]
        public void Then_a_money_transfer_event_will_be_published()
        {
            PublishedEvents.Last().WillBeOfType<MoneyTransferFailedEvent>();
        }

        [Then]
        public void Then_the_published_event_will_contain_the_amount_and_new_account_balance()
        {
            PublishedEvents.Last<MoneyTransferFailedEvent>().Amount.WillBe(5.0M);
            PublishedEvents.Last<MoneyTransferFailedEvent>().Balance.WillBe(20.0M);
            PublishedEvents.Last<MoneyTransferFailedEvent>().TargetAccount.WillBe("0987654321");
        }
    }

    public class When_sending_a_money_transfer_failed_on_an_account_that_is_not_yet_created : CommandTestFixture<MoneyTransferFailedCommand, MoneyTransferFailedCommandHandler, ActiveAccount>
    {
        protected override MoneyTransferFailedCommand When()
        {
            return new MoneyTransferFailedCommand(Guid.NewGuid(), 5.0M, "0987654321");
        }

        [Then]
        public void Then_a_non_existing_account_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<NonExitsingAccountException>();
        }

        [Then]
        public void Then_the_exception_message_will_be()
        {
            CaughtException.Message.WillBe("The ActiveAcount is not created and no opperations can be executed on it");
        }
    }

    public class When_sending_a_money_transfer_failed_on_an_account_that_is_closed : CommandTestFixture<MoneyTransferFailedCommand, MoneyTransferFailedCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new AccountClosedEvent()).ToVersion(2);
        }

        protected override MoneyTransferFailedCommand When()
        {
            return new MoneyTransferFailedCommand(Guid.NewGuid(), 5.0M, "0987654321");
        }

        [Then]
        public void Then_a_closed_account_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<ClosedAccountException>();
        }

        [Then]
        public void Then_the_exception_message_will_be()
        {
            CaughtException.Message.WillBe("The ActiveAcount is closed and no opperations can be executed on it");
        }
    }

    public class When_money_transfer_failed : EventTestFixture<MoneyTransferFailedEvent, MoneyTransferFailedEventHandler>
    {
        private static Guid _accountId;
        private object UpdateAccountDetailsObject;
        private object WhereAccountDetailsObject;
        private LedgerReport LedgerReportObject;

        protected override void SetupDependencies()
        {
            OnDependency<IReportingRepository>()
                .Setup(x => x.Update<AccountDetailsReport>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { UpdateAccountDetailsObject = u; WhereAccountDetailsObject = w; });

            OnDependency<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<LedgerReport>()))
                .Callback<LedgerReport>(l => { LedgerReportObject = l; });
        }

        protected override MoneyTransferFailedEvent When()
        {
            _accountId = Guid.NewGuid();
            return new MoneyTransferFailedEvent(50.5M, 10.5M, "0987654321") { AggregateId = _accountId };
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_update_the_account_details_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Update<AccountDetailsReport>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [Then]
        public void Then_the_account_details_report_will_be_updated_with_the_expected_details()
        {
            UpdateAccountDetailsObject.WillBeSimuliar(new { Balance = 50.5M }.ToString());
            WhereAccountDetailsObject.WillBeSimuliar(new { Id = _accountId });
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_save_the_ledger_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Save(It.IsAny<LedgerReport>()));
        }

        [Then]
        public void Then_the_ledger_report_will_be_saved_with_the_expected_details()
        {
            LedgerReportObject.AccountDetailsReportId.WillBe(_accountId);
            LedgerReportObject.Amount.WillBe(10.5M);
            LedgerReportObject.Action.WillBe("Transfer to 0987654321 failed");
        }
    }

    public class When_sending_a_money_transfer_internal_account : BaseTestFixture<MoneyTransferService>
    {
        protected override void SetupDependencies()
        {
            OnDependency<IReportingRepository>()
                .Setup(x => x.GetByExample<AccountReport>(It.IsAny<object>()))
                .Returns(new List<AccountReport> { new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "target account number") });
        }

        protected override void Given()
        {
            // !!! This is DEMO code !!!
            // Setup the SystemRandom class to return the value where the account is found
            SystemRandom.Next = (min, max) => 0;
            SystemTimer.ByPassTimer();
        }

        protected override void When()
        {
            SubjectUnderTest.Send(new MoneyTransfer("source account number", "target account number", 123.45M));
        }

        [Then]
        public void Then_the_newly_created_account_will_be_saved()
        {
            OnDependency<ICommandBus>().Verify(x => x.Publish(It.IsAny<ReceiveMoneyTransferCommand>()));
        }

        protected override void Finally()
        {
            SystemTimer.Reset();
            SystemRandom.Reset();
        }
    }

    public class When_sending_a_money_transfer_internal_account_and_it_failed : BaseTestFixture<MoneyTransferService>
    {
        protected override void SetupDependencies()
        {
            OnDependency<ICommandBus>()
                .Setup(x => x.Publish(It.IsAny<ReceiveMoneyTransferCommand>()))
                .Throws(new Exception("exception message"));

            OnDependency<IReportingRepository>()
                .Setup(x => x.GetByExample<AccountReport>(It.IsAny<object>()))
                .Returns(new List<AccountReport> { new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "target account number") });
        }

        protected override void Given()
        {
            // !!! This is DEMO code !!!
            // Setup the SystemRandom class to return the value where the account is not found
            SystemRandom.Next = (min, max) => 0;
            SystemTimer.ByPassTimer();
        }

        protected override void When()
        {
            SubjectUnderTest.Send(new MoneyTransfer("source account number", "target account number", 123.45M));
        }

        [Then]
        public void Then_the_newly_created_account_will_be_saved()
        {
            OnDependency<ICommandBus>().Verify(x => x.Publish(It.IsAny<MoneyTransferFailedCommand>()));
        }

        protected override void Finally()
        {
            SystemTimer.Reset();
            SystemRandom.Reset();
        }
    }

    public class When_sending_a_money_transfer_external_account : BaseTestFixture<MoneyTransferService>
    {
        protected override void SetupDependencies()
        {
            OnDependency<IReportingRepository>()
                .Setup(x => x.GetByExample<AccountReport>(It.IsAny<object>()))
                .Returns(new List<AccountReport> { new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "target account number") });
        }

        protected override void Given()
        {
            // !!! This is DEMO code !!!
            // Setup the SystemRandom class to return the value where the account is not found
            SystemRandom.Next = (min, max) => 2;
            SystemTimer.ByPassTimer();
        }

        protected override void When()
        {
            SubjectUnderTest.Send(new MoneyTransfer("source account number", "target account number", 123.45M));
        }

        [Then]
        public void Then_the_newly_created_account_will_be_saved()
        {
            OnDependency<IReceiveMoneyTransfers>().Verify(x => x.Receive(It.IsAny<MoneyTransfer>()));
        }

        protected override void Finally()
        {
            SystemTimer.Reset();
            SystemRandom.Reset();
        }
    }

    public class When_sending_a_money_transfer_external_account_and_it_failed : BaseTestFixture<MoneyTransferService>
    {
        protected override void SetupDependencies()
        {
            OnDependency<IReceiveMoneyTransfers>()
                .Setup(x => x.Receive(It.IsAny<MoneyTransfer>()))
                .Throws(new UnknownAccountException("exception message"));

            OnDependency<IReportingRepository>()
                .Setup(x => x.GetByExample<AccountReport>(It.IsAny<object>()))
                .Returns(new List<AccountReport> { new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "target account number") });
        }

        protected override void Given()
        {
            // !!! This is DEMO code !!!
            // Setup the SystemRandom class to return the value where the account is not found
            SystemRandom.Next = (min, max) => 4;
            SystemTimer.ByPassTimer();
        }

        protected override void When()
        {
            SubjectUnderTest.Send(new MoneyTransfer("source account number", "target account number", 123.45M));
        }

        [Then]
        public void Then_the_newly_created_account_will_be_saved()
        {
            OnDependency<ICommandBus>().Verify(x => x.Publish(It.IsAny<MoneyTransferFailedCommand>()));
        }

        protected override void Finally()
        {
            SystemTimer.Reset();
            SystemRandom.Reset();
        }
    }
}