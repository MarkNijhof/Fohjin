using System;
using System.Linq;
using Fohjin.DDD.Domain;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.Events;
using Fohjin.DDD.Domain.ValueObjects;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Fohjin.DDD.Tests.Domain.Entities
{
    [TestFixture]
    public class ActiveAccountTest
    {
        [Test]
        public void When_calling_Create_on_ActiveAccount_then_it_will_generate_an_account_create_event()
        {
            IActiveAccount activeAccount = new ActiveAccount();
            activeAccount.Create();

            var activeAccountForRepository = (IExposeMyInternalChanges) activeAccount;

            Assert.That(activeAccountForRepository.GetChanges().Count(), Is.EqualTo(1));
            Assert.That(activeAccountForRepository.GetChanges().First(), Is.InstanceOfType(typeof(AccountCreatedEvent)));
            Assert.That(((AccountCreatedEvent)activeAccountForRepository.GetChanges().First()).Guid, Is.Not.EqualTo(new Guid()));
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void When_calling_Create_twice_on_ActiveAccount_then_it_will_throw_an_exception()
        {
            IActiveAccount activeAccount = new ActiveAccount();
            activeAccount.Create();
            activeAccount.Create();
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void When_calling_Close_on_a_not_created_ActiveAccount_then_it_will_throw_an_exception()
        {
            IActiveAccount activeAccount = new ActiveAccount();
            activeAccount.Close();
        }

        [Test]
        public void When_calling_Close_on_ActiveAccount_then_it_will_generate_an_account_closed_event()
        {
            IActiveAccount activeAccount = new ActiveAccount();
            activeAccount.Create();
            activeAccount.Close();

            var activeAccountForRepository = (IExposeMyInternalChanges)activeAccount;


            Assert.That(activeAccountForRepository.GetChanges().Last(), Is.InstanceOfType(typeof(AccountClosedEvent)));
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void When_calling_Deposite_on_a_not_created_ActiveAccount_then_it_will_throw_an_exception()
        {
            IActiveAccount activeAccount = new ActiveAccount();
            activeAccount.Deposite(new Amount(0));
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void When_calling_Withdrawl_on_a_not_created_ActiveAccount_then_it_will_throw_an_exception()
        {
            IActiveAccount activeAccount = new ActiveAccount();
            activeAccount.Withdrawl(new Amount(0));
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void When_trying_to_Withdrawl_more_then_the_current_account_balance_it_will_throw_an_exception()
        {
            IActiveAccount activeAccount = new ActiveAccount();
            activeAccount.Create();
            activeAccount.Withdrawl(new Amount(10));            
        }

        [Test]
        public void When_calling_Deposite_then_it_will_generate_an_deposite_event_with_the_expected_amount()
        {
            IActiveAccount activeAccount = new ActiveAccount();
            activeAccount.Create();
            activeAccount.Deposite(new Amount(20));

            var activeAccountForRepository = (IExposeMyInternalChanges)activeAccount;

            Assert.That(activeAccountForRepository.GetChanges().Last(), Is.InstanceOfType(typeof(DepositeEvent)));
            Assert.That(((DepositeEvent)activeAccountForRepository.GetChanges().Last()).Amount.GetDecimal(), Is.EqualTo(20));
        }

        [Test]
        public void When_trying_to_Withdrawl_less_then_the_current_account_balance_it_will_generate_an_withdrawl_event_with_the_expected_amount()
        {
            IActiveAccount activeAccount = new ActiveAccount();
            activeAccount.Create();
            activeAccount.Deposite(new Amount(20));
            activeAccount.Withdrawl(new Amount(10));

            var activeAccountForRepository = (IExposeMyInternalChanges)activeAccount;

            Assert.That(activeAccountForRepository.GetChanges().Last(), Is.InstanceOfType(typeof(WithdrawlEvent)));
            Assert.That(((WithdrawlEvent)activeAccountForRepository.GetChanges().Last()).Amount.GetDecimal(), Is.EqualTo(10));
        }
    }
}