using System;
using System.Linq;
using System.Collections.Generic;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.Exceptions;
using Fohjin.DDD.Events;
using Fohjin.DDD.Events.ActiveAccount;
using Fohjin.DDD.Reporting.Dto;
using Moq;
using ClosedAccount=Fohjin.DDD.Domain.Entities.ClosedAccount;

namespace Test.Fohjin.DDD.Commands
{
    public class When_calling_Create_New_on_ActiveAccount : AggregateRootTestFixture<ActiveAccount>
    {
        private string _ticks;
        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override void When()
        {
            SystemDateTime.Now = () => new DateTime(2009, 1, 1, 1, 1, 1, 1);
            _ticks = SystemDateTime.Now().Ticks.ToString();
            aggregateRoot = ActiveAccount.CreateNew(Guid.NewGuid(), "New Account");
            SystemDateTime.Reset();
        }

        [Then]
        public void Then_it_will_generate_an_account_created_event()
        {
            events.Last().WillBeOfType<AccountCreatedEvent>();
        }

        [Then]
        public void Then_the_generated_new_account_created_event_will_contain_the_name_of_the_new_account()
        {
            events.Last<AccountCreatedEvent>().AccountName.WillBe("New Account");
        }

        [Then]
        public void Then_the_generated_new_account_created_event_will_contain_the_number_of_the_new_account()
        {
            events.Last<AccountCreatedEvent>().AccountNumber.WillBe(_ticks);
        }
    }

    public class When_providing_a_close_account_command_on_a_not_created_ActiveAccount : CommandTestFixture<CloseAccountCommand, CloseAccountCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override CloseAccountCommand When()
        {
            return new CloseAccountCommand(Guid.NewGuid());
        }

        [Then]
        public void Then_it_will_throw_an_exception()
        {
            caught.WillBeOfType<AccountWasNotCreatedException>();
        }
    }

    public class When_providing_a_close_account_command_on_a_created_ActiveAccount : CommandTestFixture<CloseAccountCommand, CloseAccountCommandHandler, ActiveAccount>
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
        public void Then_it_will_generate_an_account_created_event()
        {
            events.Last().WillBeOfType<AccountClosedEvent>();
        }

        [Then]
        public void Then_the_newly_created_closed_account_will_be_saved()
        {
            GetMock<IDomainRepository>().Verify(x => x.Save(It.IsAny<ClosedAccount>()));
        }
    }

    public class When_providing_a_close_account_command_on_a_created_ActiveAccount_with_a_positive_balance : CommandTestFixture<CloseAccountCommand, CloseAccountCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new DepositeEvent(20, 20)).ToVersion(1);
        }

        protected override CloseAccountCommand When()
        {
            return new CloseAccountCommand(Guid.NewGuid());
        }

        [Then]
        public void Then_it_will_throw_an_exception()
        {
            caught.WillBeOfType<AccountMustFirstBeEmptiedBeforeClosingException>();
        }
    }

    public class When_providing_a_close_account_command_on_a_closed_ActiveAccount : CommandTestFixture<CloseAccountCommand, CloseAccountCommandHandler, ActiveAccount>
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
        public void Then_it_will_throw_an_exception()
        {
            caught.WillBeOfType<AccountWasClosedException>();
        }
    }

    public class When_providing_a_cash_deposite_command_on_a_not_created_ActiveAccount : CommandTestFixture<CashDepositeCommand, CashDepositeCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override CashDepositeCommand When()
        {
            return new CashDepositeCommand(Guid.NewGuid(), 0);
        }

        [Then]
        public void Then_it_will_throw_an_exception()
        {
            caught.WillBeOfType<AccountWasNotCreatedException>();
        }
    }

    public class When_providing_a_cash_deposite_command_on_a_closed_ActiveAccount : CommandTestFixture<CashDepositeCommand, CashDepositeCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new AccountClosedEvent()).ToVersion(2);
        }

        protected override CashDepositeCommand When()
        {
            return new CashDepositeCommand(Guid.NewGuid(), 0);
        }

        [Then]
        public void Then_it_will_throw_an_exception()
        {
            caught.WillBeOfType<AccountWasClosedException>();
        }
    }

    public class When_providing_a_cash_deposite_command_on_a_created_ActiveAccount : CommandTestFixture<CashDepositeCommand, CashDepositeCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new DepositeEvent(10, 10)).ToVersion(2);
        }

        protected override CashDepositeCommand When()
        {
            return new CashDepositeCommand(Guid.NewGuid(), 20);
        }

        [Then]
        public void Then_it_will_generate_an_deposite_event()
        {
            events.Last().WillBeOfType<DepositeEvent>();
        }

        [Then]
        public void Then_it_will_generate_an_deposite_event_with_the_expected_new_balance()
        {
            events.Last<DepositeEvent>().Balance.WillBe(30);
        }

        [Then]
        public void Then_it_will_generate_an_deposite_event_with_the_expected_ammount()
        {
            events.Last<DepositeEvent>().Amount.WillBe(20);
        }
    }

    public class When_providing_a_cash_withdrawl_command_on_a_not_created_ActiveAccount : CommandTestFixture<CashWithdrawlCommand, CashWithdrawlCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override CashWithdrawlCommand When()
        {
            return new CashWithdrawlCommand(Guid.NewGuid(), 0);
        }

        [Then]
        public void Then_it_will_throw_an_exception()
        {
            caught.WillBeOfType<AccountWasNotCreatedException>();
        }
    }

    public class When_providing_a_cash_withdrawl_command_on_a_closed_ActiveAccount : CommandTestFixture<CashWithdrawlCommand, CashWithdrawlCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new AccountClosedEvent()).ToVersion(2);
        }

        protected override CashWithdrawlCommand When()
        {
            return new CashWithdrawlCommand(Guid.NewGuid(), 0);
        }

        [Then]
        public void Then_it_will_throw_an_exception()
        {
            caught.WillBeOfType<AccountWasClosedException>();
        }
    }

    public class When_providing_a_cash_withdrawl_command_with_not_enough_balance_on_the_ActiveAccount : CommandTestFixture<CashWithdrawlCommand, CashWithdrawlCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
        }

        protected override CashWithdrawlCommand When()
        {
            return new CashWithdrawlCommand(Guid.NewGuid(), 1);
        }

        [Then]
        public void Then_it_will_throw_an_exception()
        {
            caught.WillBeOfType<AccountBalanceIsToLowException>();
        }

        [Then]
        public void Then_it_will_throw_an_exception_with_a_specified_message()
        {
            caught.WithMessage(string.Format("The amount {0} is larger than your current balance {1}", 1, 0));
        }
    }

    public class When_providing_a_cash_withdrawl_command_with_enough_balance_on_the_ActiveAccount : CommandTestFixture<CashWithdrawlCommand, CashWithdrawlCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new DepositeEvent(20, 20)).ToVersion(1);
        }

        protected override CashWithdrawlCommand When()
        {
            return new CashWithdrawlCommand(Guid.NewGuid(), 5);
        }

        [Then]
        public void Then_it_will_generate_an_deposite_event()
        {
            events.Last().WillBeOfType<WithdrawlEvent>();
        }

        [Then]
        public void Then_it_will_generate_an_deposite_event_with_the_expected_new_balance()
        {
            events.Last<WithdrawlEvent>().Balance.WillBe(15);
        }

        [Then]
        public void Then_it_will_generate_an_deposite_event_with_the_expected_ammount()
        {
            events.Last<WithdrawlEvent>().Amount.WillBe(5);
        }
    }

    public class When_providing_an_account_name_got_changed_command_on_a_not_created_ActiveAccount : CommandTestFixture<AccountNameGotChangedCommand, AccountNameGotChangedCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override AccountNameGotChangedCommand When()
        {
            return new AccountNameGotChangedCommand(Guid.NewGuid(), "New Account Name");
        }

        [Then]
        public void Then_it_will_throw_an_exception()
        {
            caught.WillBeOfType<AccountWasNotCreatedException>();
        }
    }

    public class When_providing_an_account_name_got_changed_command_on_a_created_ActiveAccount : CommandTestFixture<AccountNameGotChangedCommand, AccountNameGotChangedCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
        }

        protected override AccountNameGotChangedCommand When()
        {
            return new AccountNameGotChangedCommand(Guid.NewGuid(), "New Account Name");
        }

        [Then]
        public void Then_it_will_generate_an_account_created_event()
        {
            events.Last().WillBeOfType<AccountNameGotChangedEvent>();
        }

        [Then]
        public void Then_it_will_generate_an_deposite_event_with_the_expected_new_account_name()
        {
            events.Last<AccountNameGotChangedEvent>().AccountName.WillBe("New Account Name");
        }
    }

    public class When_providing_an_account_name_got_changed_command_on_a_closed_ActiveAccount : CommandTestFixture<AccountNameGotChangedCommand, AccountNameGotChangedCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new AccountClosedEvent()).ToVersion(2);
        }

        protected override AccountNameGotChangedCommand When()
        {
            return new AccountNameGotChangedCommand(Guid.NewGuid(), "New Account Name");
        }

        [Then]
        public void Then_it_will_throw_an_exception()
        {
            caught.WillBeOfType<AccountWasClosedException>();
        }
    }

    public class When_providing_a_transfer_to_an_other_account_command_on_a_not_created_ActiveAccount : CommandTestFixture<TransferMoneyToAnOtherAccountCommand, TransferMoneyToAnOtherAccountCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override TransferMoneyToAnOtherAccountCommand When()
        {
            return new TransferMoneyToAnOtherAccountCommand(Guid.NewGuid(), 10.0M, "1234567890");
        }

        [Then]
        public void Then_it_will_throw_an_exception()
        {
            caught.WillBeOfType<AccountWasNotCreatedException>();
        }
    }

    public class When_providing_a_transfer_to_an_other_account_command_with_not_enough_balance_on_the_ActiveAccount : CommandTestFixture<TransferMoneyToAnOtherAccountCommand, TransferMoneyToAnOtherAccountCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
        }

        protected override TransferMoneyToAnOtherAccountCommand When()
        {
            return new TransferMoneyToAnOtherAccountCommand(Guid.NewGuid(), 10.0M, "1234567890");
        }

        [Then]
        public void Then_it_will_throw_an_exception()
        {
            caught.WillBeOfType<AccountBalanceIsToLowException>();
        }

        [Then]
        public void Then_it_will_throw_an_exception_with_a_specified_message()
        {
            caught.WithMessage(string.Format("The amount {0} is larger than your current balance {1}", 10.0M, 0));
        }
    }

    public class When_providing_a_transfer_to_an_other_account_command_with_enough_balance_on_the_ActiveAccount : CommandTestFixture<TransferMoneyToAnOtherAccountCommand, TransferMoneyToAnOtherAccountCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new DepositeEvent(20, 20)).ToVersion(1);
        }

        protected override TransferMoneyToAnOtherAccountCommand When()
        {
            return new TransferMoneyToAnOtherAccountCommand(Guid.NewGuid(), 5.0M, "1234567890");
        }

        [Then]
        public void Then_it_will_generate_an_account_created_event()
        {
            events.Last().WillBeOfType<MoneyTransferSendToAnOtherAccountEvent>();
        }

        [Then]
        public void Then_it_will_generate_an_deposite_event_with_the_expected_ammount()
        {
            events.Last<MoneyTransferSendToAnOtherAccountEvent>().Amount.WillBe(5.0M);
        }

        [Then]
        public void Then_it_will_generate_an_deposite_event_with_the_expected_balance()
        {
            events.Last<MoneyTransferSendToAnOtherAccountEvent>().Balance.WillBe(15.0M);
        }

        [Then]
        public void Then_it_will_generate_an_deposite_event_with_the_expected_other_account()
        {
            events.Last<MoneyTransferSendToAnOtherAccountEvent>().TargetAccount.WillBe("1234567890");
        }
    }

    public class When_providing_a_transfer_to_an_other_account_command_on_a_closed_ActiveAccount : CommandTestFixture<TransferMoneyToAnOtherAccountCommand, TransferMoneyToAnOtherAccountCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new AccountClosedEvent()).ToVersion(2);
        }

        protected override TransferMoneyToAnOtherAccountCommand When()
        {
            return new TransferMoneyToAnOtherAccountCommand(Guid.NewGuid(), 10.0M, "1234567890");
        }

        [Then]
        public void Then_it_will_throw_an_exception()
        {
            caught.WillBeOfType<AccountWasClosedException>();
        }
    }

    public class When_providing_a_transfer_from_an_other_account_command_on_a_not_created_ActiveAccount : CommandTestFixture<TransferMoneyFromAnOtherAccountCommand, TransferMoneyFromAnOtherAccountCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override TransferMoneyFromAnOtherAccountCommand When()
        {
            return new TransferMoneyFromAnOtherAccountCommand(Guid.NewGuid(), 10.0M, "1234567890");
        }

        [Then]
        public void Then_it_will_throw_an_exception()
        {
            caught.WillBeOfType<AccountWasNotCreatedException>();
        }
    }

    public class When_providing_a_transfer_from_an_other_account_command_on_a_created_ActiveAccount : CommandTestFixture<TransferMoneyFromAnOtherAccountCommand, TransferMoneyFromAnOtherAccountCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new DepositeEvent(20, 20)).ToVersion(1);
        }

        protected override TransferMoneyFromAnOtherAccountCommand When()
        {
            return new TransferMoneyFromAnOtherAccountCommand(Guid.NewGuid(), 5.0M, "1234567890");
        }

        [Then]
        public void Then_it_will_generate_an_account_created_event()
        {
            events.Last().WillBeOfType<MoneyTransferReceivedFromAnOtherAccountEvent>();
        }

        [Then]
        public void Then_it_will_generate_an_deposite_event_with_the_expected_ammount()
        {
            events.Last<MoneyTransferReceivedFromAnOtherAccountEvent>().Amount.WillBe(5.0M);
        }

        [Then]
        public void Then_it_will_generate_an_deposite_event_with_the_expected_balance()
        {
            events.Last<MoneyTransferReceivedFromAnOtherAccountEvent>().Balance.WillBe(25.0M);
        }

        [Then]
        public void Then_it_will_generate_an_deposite_event_with_the_expected_other_account()
        {
            events.Last<MoneyTransferReceivedFromAnOtherAccountEvent>().TargetAccount.WillBe("1234567890");
        }
    }

    public class When_providing_a_transfer_from_an_other_account_command_on_a_closed_ActiveAccount : CommandTestFixture<TransferMoneyFromAnOtherAccountCommand, TransferMoneyFromAnOtherAccountCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new AccountClosedEvent()).ToVersion(2);
        }

        protected override TransferMoneyFromAnOtherAccountCommand When()
        {
            return new TransferMoneyFromAnOtherAccountCommand(Guid.NewGuid(), 10.0M, "1234567890");
        }

        [Then]
        public void Then_it_will_throw_an_exception()
        {
            caught.WillBeOfType<AccountWasClosedException>();
        }
    }
}