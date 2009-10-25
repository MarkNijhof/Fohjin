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
using Fohjin.DDD.Services;
using Moq;

namespace Test.Fohjin.DDD.Scenarios
{
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
        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override SendMoneyTransferCommand When()
        {
            return new SendMoneyTransferCommand(Guid.NewGuid(), 10.0M, "1234567890");
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
        public void Then_an_account_balance_is_to_low_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<AccountBalanceIsToLowException>();
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

        protected override void MockSetup()
        {
            GetMock<IReportingRepository>()
                .Setup(x => x.Update<AccountDetailsReport>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { UpdateAccountDetailsObject = u; WhereAccountDetailsObject = w; });

            GetMock<IReportingRepository>()
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
            GetMock<IReportingRepository>().Verify(x => x.Update<AccountDetailsReport>(It.IsAny<object>(), It.IsAny<object>()));
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
            GetMock<IReportingRepository>().Verify(x => x.Save(It.IsAny<LedgerReport>()));
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

        protected override void MockSetup()
        {
            GetMock<ISendMoneyTransfer>()
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
            GetMock<ISendMoneyTransfer>().Verify(x => x.Send(It.IsAny<MoneyTransfer>()));
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
        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override MoneyTransferFailedCommand When()
        {
            return new MoneyTransferFailedCommand(Guid.NewGuid(), 5.0M, "0987654321");
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

    public class When_money_transfer_was_failed : EventTestFixture<MoneyTransferFailedEvent, MoneyTransferFailedEventHandler>
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

        protected override MoneyTransferFailedEvent When()
        {
            _accountId = Guid.NewGuid();
            return new MoneyTransferFailedEvent(50.5M, 10.5M, "0987654321") { AggregateId = _accountId };
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
            LedgerReportObject.Action.WillBe("Transfer to 0987654321 failed");
        }
    }
}