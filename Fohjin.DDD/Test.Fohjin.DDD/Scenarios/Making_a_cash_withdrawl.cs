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
        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override WithdrawlCashCommand When()
        {
            return new WithdrawlCashCommand(Guid.NewGuid(), 0);
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
        public void Then_an_account_balance_is_to_low_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<AccountBalanceIsToLowException>();
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

        protected override void MockSetup()
        {
            GetMock<IReportingRepository>()
                .Setup(x => x.Update<AccountDetailsReport>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { UpdateAccountDetailsObject = u; WhereAccountDetailsObject = w; });

            GetMock<IReportingRepository>()
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
            GetMock<IReportingRepository>().Verify(x => x.Update<AccountDetailsReport>(It.IsAny<object>(), It.IsAny<object>()));
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
            GetMock<IReportingRepository>().Verify(x => x.Save(It.IsAny<LedgerReport>()));
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