using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Configuration;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.Domain.Mementos;
using Fohjin.DDD.EventStore.SQLite;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Test.Fohjin.DDD.Domain.Repositories
{
    [TestFixture]
    public class ClosedAccountRepositoryTest
    {
        private const string dataBaseFile = "domainDataBase.db3";

        private IDomainRepository _repository;
        private Storage _storage;
        private List<Ledger> _ledgers;

        [SetUp]
        public void SetUp()
        {
            new DomainDatabaseBootStrapper().ReCreateDatabaseSchema();

            var sqliteConnectionString = string.Format("Data Source={0}", dataBaseFile);

            _storage = new Storage(sqliteConnectionString, new BinaryFormatter());

            _repository = new SQLiteDomainRepository(_storage, _storage, new Mock<IEventBus>().Object);
        }

        [Test]
        public void When_calling_Save_it_will_add_the_domain_events_to_the_domain_event_storage()
        {
            _ledgers = new List<Ledger>
            {
                new CreditMutation(1, new AccountNumber("0987654321")),
                new DebitMutation(1, new AccountNumber("0987654321")),
                new CreditTransfer(1, new AccountNumber("0987654321")),
                new DebitTransfer(1, new AccountNumber("0987654321")),
                new DebitTransferFailed(1, new AccountNumber("0987654321")),
            };

            var closedAccount = ClosedAccount.CreateNew(Guid.NewGuid(), Guid.NewGuid(), _ledgers, new AccountName("AccountName"), new AccountNumber("1234567890"));

            _repository.Save(closedAccount);

            Assert.That(_storage.GetEventsSinceLastSnapShot(closedAccount.Id).Count(), Is.EqualTo(1));
            Assert.That(_storage.GetAllEvents(closedAccount.Id).Count(), Is.EqualTo(1));
        }

        [Test]
        public void When_calling_Save_it_will_reset_the_domain_events()
        {
            _ledgers = new List<Ledger>
            {
                new CreditMutation(1, new AccountNumber("0987654321")),
                new DebitMutation(1, new AccountNumber("0987654321")),
                new CreditTransfer(1, new AccountNumber("0987654321")),
                new DebitTransfer(1, new AccountNumber("0987654321")),
                new DebitTransferFailed(1, new AccountNumber("0987654321")),
            };

            var closedAccount = ClosedAccount.CreateNew(Guid.NewGuid(), Guid.NewGuid(), _ledgers, new AccountName("AccountName"), new AccountNumber("1234567890"));

            _repository.Save(closedAccount);

            var closedAccountForRepository = (IEventProvider)closedAccount;

            Assert.That(closedAccountForRepository.GetChanges().Count(), Is.EqualTo(0));
        }

        [Test]
        public void When_calling_CreateMemento_it_will_return_a_closed_account_memento()
        {
            _ledgers = new List<Ledger>
            {
                new CreditMutation(1, new AccountNumber("0987654321")),
                new DebitMutation(1, new AccountNumber("0987654321")),
                new CreditTransfer(1, new AccountNumber("0987654321")),
                new DebitTransfer(1, new AccountNumber("0987654321")),
                new DebitTransferFailed(1, new AccountNumber("0987654321")),
            };

            var closedAccount = ClosedAccount.CreateNew(Guid.NewGuid(), Guid.NewGuid(), _ledgers, new AccountName("AccountName"), new AccountNumber("1234567890"));

            var memento = ((IOrginator)closedAccount).CreateMemento();

            var newClosedAccount = new ClosedAccount();

            ((IOrginator)newClosedAccount).SetMemento(memento);

            ClosedAccountComparer(closedAccount, newClosedAccount);
        }

        private static void ClosedAccountComparer(ClosedAccount original, ClosedAccount recreated)
        {
            var fields = typeof(ClosedAccount).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (var field in fields)
            {
                if (field.FieldType == typeof(List<Ledger>))
                {
                    var counter = 0;
                    var ledgers = (List<Ledger>)field.GetValue(recreated);
                    foreach (var ledger in (List<Ledger>)field.GetValue(original))
                    {
                        Assert.That(ledger.ToString(), Is.EqualTo(ledgers[counter++].ToString()));
                    }
                    continue;
                }
                Assert.That(field.GetValue(original).ToString(), Is.EqualTo(field.GetValue(recreated).ToString()));
            }
        }


    }
}