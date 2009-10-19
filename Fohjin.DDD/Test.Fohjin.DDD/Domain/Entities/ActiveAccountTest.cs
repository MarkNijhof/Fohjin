//using System;
//using System.Linq;
//using System.Collections.Generic;
//using Fohjin.DDD.Events;
//using Fohjin.DDD.Domain.Entities;
//using Fohjin.DDD.Domain.ValueObjects;
//using Fohjin.DDD.Events.ActiveAccount;

//namespace Test.Fohjin.DDD.Domain.Entities
//{
//    public class When_calling_Close_on_a_not_created_ActiveAccount : AggregateRootTestFixture<ActiveAccount>
//    {
//        protected override IEnumerable<IDomainEvent> Given()
//        {
//            return new List<IDomainEvent>();
//        }

//        protected override void When()
//        {
//            aggregateRoot.Close();
//        }

//        [Then]
//        public void Then_it_will_throw_an_exception()
//        {
//            caught.WillBeOfType<Exception>();
//        }
//    }
//    public class When_calling_Deposite_on_a_not_created_ActiveAccount : AggregateRootTestFixture<ActiveAccount>
//    {
//        protected override IEnumerable<IDomainEvent> Given()
//        {
//            return new List<IDomainEvent>();
//        }

//        protected override void When()
//        {
//            aggregateRoot.Deposite(0);
//        }

//        [Then]
//        public void Then_it_will_throw_an_exception()
//        {
//            caught.WillBeOfType<Exception>();
//        }
//    }
//    public class When_calling_Withdrawl_on_a_not_created_ActiveAccount : AggregateRootTestFixture<ActiveAccount>
//    {
//        protected override IEnumerable<IDomainEvent> Given()
//        {
//            return new List<IDomainEvent>();
//        }

//        protected override void When()
//        {
//            aggregateRoot.Withdrawl(0);
//        }

//        [Then]
//        public void Then_it_will_throw_an_exception()
//        {
//            caught.WillBeOfType<Exception>();
//        }
//    }
//    public class When_calling_Close_on_a_created_ActiveAccount : AggregateRootTestFixture<ActiveAccount>
//    {
//        protected override IEnumerable<IDomainEvent> Given()
//        {
//            yield return new AccountCreatedEvent(Guid.NewGuid(), "AccountName");
//        }

//        protected override void When()
//        {
//            aggregateRoot.Close();
//        }

//        [Then]
//        public void Then_it_will_generate_an_account_created_event()
//        {
//            events.Last().WillBeOfType<AccountClosedEvent>();
//        }
//    }
//    public class When_calling_Withdrawl_with_not_enough_balance_on_the_ActiveAccount : AggregateRootTestFixture<ActiveAccount>
//    {
//        protected override IEnumerable<IDomainEvent> Given()
//        {
//            yield return new AccountCreatedEvent(Guid.NewGuid(), "AccountName");
//        }

//        protected override void When()
//        {
//            aggregateRoot.Withdrawl(1);
//        }

//        [Then]
//        public void Then_it_will_throw_an_exception()
//        {
//            caught.WillBeOfType<Exception>();
//        }

//        [Then]
//        public void Then_it_will_throw_an_exception_with_a_specified_message()
//        {
//            caught.WithMessage(string.Format("The amount {0} is larger than your current balance {1}", 1, 0));
//        }
//    }
//    public class When_calling_Deposite_with_a_specific_amount_on_the_ActiveAccount : AggregateRootTestFixture<ActiveAccount>
//    {
//        protected override IEnumerable<IDomainEvent> Given()
//        {
//            yield return new AccountCreatedEvent(Guid.NewGuid(), "AccountName");
//            yield return new DepositeEvent(10, 10);
//        }

//        protected override void When()
//        {
//            aggregateRoot.Deposite(new Amount(20));
//        }

//        [Then]
//        public void Then_it_will_generate_an_deposite_event()
//        {
//            events.Last().WillBeOfType<DepositeEvent>();
//        }

//        [Then]
//        public void Then_it_will_generate_an_deposite_event_with_the_expected_new_balance()
//        {
//            events.Last<DepositeEvent>().Balance.WillBe(30);
//        }

//        [Then]
//        public void Then_it_will_generate_an_deposite_event_with_the_expected_ammount()
//        {
//            events.Last<DepositeEvent>().Amount.WillBe(20);
//        }
//    }
//    public class When_calling_Withdrawl_with_enough_balance_on_the_ActiveAccount : AggregateRootTestFixture<ActiveAccount>
//    {
//        protected override IEnumerable<IDomainEvent> Given()
//        {
//            yield return new AccountCreatedEvent(Guid.NewGuid(), "AccountName");
//            yield return new DepositeEvent(20, 20);
//        }

//        protected override void When()
//        {
//            aggregateRoot.Withdrawl(new Amount(5));
//        }

//        [Then]
//        public void Then_it_will_generate_an_deposite_event()
//        {
//            events.Last().WillBeOfType<WithdrawlEvent>();
//        }

//        [Then]
//        public void Then_it_will_generate_an_deposite_event_with_the_expected_new_balance()
//        {
//            events.Last<WithdrawlEvent>().Balance.WillBe(15);
//        }

//        [Then]
//        public void Then_it_will_generate_an_deposite_event_with_the_expected_ammount()
//        {
//            events.Last<WithdrawlEvent>().Amount.WillBe(5);
//        }
//    }
//}