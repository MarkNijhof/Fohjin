using System;
using System.Linq;
using System.Collections.Generic;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Bus;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events;
using Fohjin.DDD.Events.Account;
using Fohjin.DDD.Reporting.Dto;
using Moq;

namespace Test.Fohjin.DDD.Scenarios
{
    public class When_clicking_to_make_a_cash_withdrawl : PresenterTestFixture<AccountDetailsPresenter>
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
        }

        protected override void Given()
        {
            Presenter.SetAccount(new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "Account name", "1234567890"));
            Presenter.Display();
        }

        protected override void When()
        {
            On<IAccountDetailsView>().FireEvent(x => x.OnInitiateMoneyWithdrawl += null);
        }

        [Then]
        public void Then_the_current_amount_is_set_to_zero()
        {
            On<IAccountDetailsView>().VerifyThat.ValueIsSetFor(x => x.WithdrawlAmount = 0M);
        }

        [Then]
        public void Then_the_save_button_will_be_disabled()
        {
            On<IAccountDetailsView>().VerifyThat.Method(x => x.DisableMenuButtons()).WasCalled();
        }

        [Then]
        public void Then_the_withdrawl_panel_will_be_enabled()
        {
            On<IAccountDetailsView>().VerifyThat.Method(x => x.EnableWithdrawlPanel()).WasCalled();
        }
    }

    public class When_inserting_the_withdrawl_ammount : PresenterTestFixture<AccountDetailsPresenter>
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
            On<IAccountDetailsView>().ValueFor(x => x.WithdrawlAmount).IsSetTo(12.3M);
            On<IAccountDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
        }

        [Then]
        public void Then_the_save_button_will_be_enabled()
        {
            On<IAccountDetailsView>().VerifyThat.Method(x => x.EnableSaveButton()).WasCalled();
        }
    }

    public class When_clearing_the_withdrawl_ammount : PresenterTestFixture<AccountDetailsPresenter>
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
        }

        protected override void Given()
        {
            Presenter.SetAccount(new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "Account name", "1234567890"));
            Presenter.Display();
            On<IAccountDetailsView>().ValueFor(x => x.AccountName).IsSetTo("Account name");
            On<IAccountDetailsView>().ValueFor(x => x.WithdrawlAmount).IsSetTo(0M);
            On<IAccountDetailsView>().ValueFor(x => x.DepositeAmount).IsSetTo(0M);
            On<IAccountDetailsView>().ValueFor(x => x.TransferAmount).IsSetTo(0M);
            On<IAccountDetailsView>().ValueFor(x => x.WithdrawlAmount).IsSetTo(12.3M);
            On<IAccountDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
        }

        protected override void When()
        {
            On<IAccountDetailsView>().ValueFor(x => x.WithdrawlAmount).IsSetTo(0M);
            On<IAccountDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
        }

        [Then]
        public void Then_the_save_button_will_be_enabled()
        {
            On<IAccountDetailsView>().VerifyThat.Method(x => x.DisableSaveButton()).WasCalled();
        }
    }

    public class When_executing_the_cash_withdrawl : PresenterTestFixture<AccountDetailsPresenter>
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
        }

        protected override void Given()
        {
            Presenter.SetAccount(new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "Account name", "1234567890"));
            Presenter.Display();
            On<IAccountDetailsView>().ValueFor(x => x.AccountName).IsSetTo("Account name");
            On<IAccountDetailsView>().ValueFor(x => x.WithdrawlAmount).IsSetTo(0M);
            On<IAccountDetailsView>().ValueFor(x => x.DepositeAmount).IsSetTo(0M);
            On<IAccountDetailsView>().ValueFor(x => x.TransferAmount).IsSetTo(0M);
            On<IAccountDetailsView>().FireEvent(x => x.OnInitiateMoneyWithdrawl += null);
            On<IAccountDetailsView>().ValueFor(x => x.WithdrawlAmount).IsSetTo(12.3M);
            On<IAccountDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
        }

        protected override void When()
        {
            On<IAccountDetailsView>().FireEvent(x => x.OnWithdrawlMoney += null);
        }

        [Then]
        public void Then_a_change_account_name_command_will_be_published()
        {
            On<ICommandBus>().VerifyThat.Method(x => x.Publish(It.IsAny<WithdrawlCashCommand>())).WasCalled();
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

    public class When_canceling_to_make_a_cash_withdrawl : PresenterTestFixture<AccountDetailsPresenter>
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

    public class When_withdrawling_cash : CommandTestFixture<WithdrawlCashCommand, WithdrawlCashCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new CashDepositedEvent(20, 20)).ToVersion(1);
        }

        protected override WithdrawlCashCommand When()
        {
            return new WithdrawlCashCommand(Guid.NewGuid(), 5);
        }

        [Then]
        public void Then_a_cash_withdrawn_event_will_be_published()
        {
            PublishedEvents.Last().WillBeOfType<CashWithdrawnEvent>();
        }

        [Then]
        public void Then_the_published_event_will_contain_the_amount_and_new_account_balance()
        {
            PublishedEvents.Last<CashWithdrawnEvent>().Balance.WillBe(15);
            PublishedEvents.Last<CashWithdrawnEvent>().Amount.WillBe(5);
        }
    }

    public class When_withdrawling_cash_from_an_account_that_is_not_yet_created : CommandTestFixture<WithdrawlCashCommand, WithdrawlCashCommandHandler, ActiveAccount>
    {
        protected override WithdrawlCashCommand When()
        {
            return new WithdrawlCashCommand(Guid.NewGuid(), 0);
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

    public class When_withdrawling_cash_from_an_account_that_is_closed : CommandTestFixture<WithdrawlCashCommand, WithdrawlCashCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new AccountClosedEvent()).ToVersion(2);
        }

        protected override WithdrawlCashCommand When()
        {
            return new WithdrawlCashCommand(Guid.NewGuid(), 0);
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

    public class When_withdrawling_cash_from_an_account_account_with_to_little_balance : CommandTestFixture<WithdrawlCashCommand, WithdrawlCashCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
        }

        protected override WithdrawlCashCommand When()
        {
            return new WithdrawlCashCommand(Guid.NewGuid(), 1);
        }

        [Then]
        public void Then_an_account_balance_to_low_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<AccountBalanceToLowException>();
        }

        [Then]
        public void Then_the_exception_message_will_be()
        {
            CaughtException.WithMessage(string.Format("The amount {0:C} is larger than your current balance {1:C}", 1, 0));
        }
    }

    public class When_cash_was_withdrawn : EventTestFixture<CashWithdrawnEvent, CashWithdrawnEventHandler>
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

        protected override CashWithdrawnEvent When()
        {
            _accountId = Guid.NewGuid();
            return new CashWithdrawnEvent(50.5M, 10.5M) { AggregateId = _accountId };
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
            LedgerReportObject.Action.WillBe("Withdrawl");
        }
    }
}