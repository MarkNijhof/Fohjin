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
    public class When_receiveing_a_money_transfer : CommandTestFixture<ReceiveMoneyTransferCommand, ReceiveMoneyTransferCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new CashDepositedEvent(20, 20)).ToVersion(1);
        }

        protected override ReceiveMoneyTransferCommand When()
        {
            return new ReceiveMoneyTransferCommand(Guid.NewGuid(), 5.0M, "0987654321");
        }

        [Then]
        public void Then_a_money_transfer_received_event_will_be_published()
        {
            PublishedEvents.Last().WillBeOfType<MoneyTransferReceivedEvent>();
        }

        [Then]
        public void Then_it_will_generate_an_deposite_event_with_the_expected_ammount()
        {
            PublishedEvents.Last<MoneyTransferReceivedEvent>().Amount.WillBe(5.0M);
            PublishedEvents.Last<MoneyTransferReceivedEvent>().Balance.WillBe(25.0M);
            PublishedEvents.Last<MoneyTransferReceivedEvent>().TargetAccount.WillBe("1234567890");
            PublishedEvents.Last<MoneyTransferReceivedEvent>().SourceAccount.WillBe("0987654321");
        }
    }

    public class When_receiveing_a_money_transfer_on_an_account_that_is_not_yet_created : CommandTestFixture<ReceiveMoneyTransferCommand, ReceiveMoneyTransferCommandHandler, ActiveAccount>
    {
        protected override ReceiveMoneyTransferCommand When()
        {
            return new ReceiveMoneyTransferCommand(Guid.NewGuid(), 10.0M, "1234567890");
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

    public class When_receiveing_a_money_transfer_on_an_account_that_is_closed : CommandTestFixture<ReceiveMoneyTransferCommand, ReceiveMoneyTransferCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new AccountClosedEvent()).ToVersion(2);
        }

        protected override ReceiveMoneyTransferCommand When()
        {
            return new ReceiveMoneyTransferCommand(Guid.NewGuid(), 10.0M, "1234567890");
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

    public class When_money_transfer_was_received : EventTestFixture<MoneyTransferReceivedEvent, MoneyTransferReceivedEventHandler>
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

        protected override MoneyTransferReceivedEvent When()
        {
            _accountId = Guid.NewGuid();
            return new MoneyTransferReceivedEvent(50.5M, 10.5M, "0987654321", "1234567890") { AggregateId = _accountId };
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
            LedgerReportObject.Action.WillBe("Transfer from 0987654321");
        }
    }
}