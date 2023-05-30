using System;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Fohjin.DDD.BankApplication;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Configuration;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.Domain.Mementos;
using Fohjin.DDD.EventStore;
using Fohjin.DDD.EventStore.SQLite;
using Fohjin.DDD.EventStore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Domain.Repositories
{
    [TestClass]
    public class ActiveAccountRepositoryTest
    {
        private const string dataBaseFile = "domainDataBase.db3";

        private IDomainRepository<IDomainEvent> _repository;
        private DomainEventStorage<IDomainEvent> _domainEventStorage;
        private EventStoreIdentityMap<IDomainEvent> _eventStoreIdentityMap;
        private EventStoreUnitOfWork<IDomainEvent> _eventStoreUnitOfWork;

        [TestInitialize]
        public void SetUp()
        {
            new DomainDatabaseBootStrapper().ReCreateDatabaseSchema();

            var sqliteConnectionString = string.Format("Data Source={0}", dataBaseFile);

            _domainEventStorage = new DomainEventStorage<IDomainEvent>(sqliteConnectionString, new BinaryFormatter());
            _eventStoreIdentityMap = new EventStoreIdentityMap<IDomainEvent>();
            _eventStoreUnitOfWork = new EventStoreUnitOfWork<IDomainEvent>(_domainEventStorage, _eventStoreIdentityMap, new Mock<IBus>().Object);
            _repository = new DomainRepository<IDomainEvent>(_eventStoreUnitOfWork, _eventStoreIdentityMap);
        }

        [TestMethod]
        public void When_calling_Save_it_will_add_the_domain_events_to_the_domain_event_storage()
        {
            var activeAccount = ActiveAccount.CreateNew(Guid.NewGuid(), "AccountName", "Account Number");
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));

            _repository.Add(activeAccount);
            _eventStoreUnitOfWork.Commit();

            Assert.AreEqual(3, _domainEventStorage.GetEventsSinceLastSnapShot(activeAccount.Id).Count());
            Assert.AreEqual(3, _domainEventStorage.GetAllEvents(activeAccount.Id).Count());
        }

        [TestMethod]
        public void When_calling_Save_it_will_reset_the_domain_events()
        {
            var activeAccount = ActiveAccount.CreateNew(Guid.NewGuid(), "AccountName", "Account Number");
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));

            _repository.Add(activeAccount);
            _eventStoreUnitOfWork.Commit();

            var activeAccountForRepository = (IEventProvider<IDomainEvent>)activeAccount;

            Assert.AreEqual(0, activeAccountForRepository.GetChanges().Count());
        }

        [TestMethod]
        public void When_calling_Save_after_more_than_9_events_a_new_snap_shot_will_be_created_9_events_will_not()
        {
            var activeAccount = ActiveAccount.CreateNew(Guid.NewGuid(), "AccountName", "Account Number");
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));

            _repository.Add(activeAccount);
            _eventStoreUnitOfWork.Commit();

            Assert.IsNull(_domainEventStorage.GetSnapShot(activeAccount.Id));
        }

        [TestMethod]
        public void When_calling_Save_after_more_than_9_events_a_new_snap_shot_will_be_created_10_events()
        {
            var activeAccount = ActiveAccount.CreateNew(Guid.NewGuid(), "AccountName", "Account Number");
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));

            _repository.Add(activeAccount);
            _eventStoreUnitOfWork.Commit();
            _domainEventStorage.SaveShapShot(activeAccount);

            var snapShot = _domainEventStorage.GetSnapShot(activeAccount.Id);

            Assert.IsNotNull(snapShot);
            Assert.IsInstanceOfType<ActiveAccountMemento>(snapShot.Memento);
        }

        [TestMethod]
        public void When_calling_Save_after_more_than_9_events_a_new_snap_shot_will_be_created_11_events()
        {
            var activeAccount = ActiveAccount.CreateNew(Guid.NewGuid(), "AccountName", "Account Number");
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));

            _repository.Add(activeAccount);
            _eventStoreUnitOfWork.Commit();
            _domainEventStorage.SaveShapShot(activeAccount);

            var snapShot = _domainEventStorage.GetSnapShot(activeAccount.Id);

            Assert.IsNotNull(snapShot);
            Assert.IsInstanceOfType<ActiveAccountMemento>(snapShot.Memento);
        }

        [TestMethod]
        public void When_calling_Save_after_more_than_9_events_after_the_last_snap_shot_a_new_snapshot_will_be_created_10_events_after_last_snapshot()
        {
            var activeAccount = ActiveAccount.CreateNew(Guid.NewGuid(), "AccountName", "Account Number");
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));

            _repository.Add(activeAccount);
            _eventStoreUnitOfWork.Commit();
            _domainEventStorage.SaveShapShot(activeAccount);

            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));

            _repository.Add(activeAccount);
            _eventStoreUnitOfWork.Commit();

            var snapShot = _domainEventStorage.GetSnapShot(activeAccount.Id);

            Assert.IsNotNull(snapShot);
            Assert.IsInstanceOfType<ActiveAccountMemento>(snapShot.Memento);
        }

        [TestMethod]
        public void When_calling_Save_after_more_than_9_events_after_the_last_snap_shot_a_new_snapshot_will_be_created_10_events_after_last_snapshot_9_events_after_last_snapshot()
        {
            var activeAccount = ActiveAccount.CreateNew(Guid.NewGuid(), "AccountName", "Account Number");
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));

            _repository.Add(activeAccount);
            _eventStoreUnitOfWork.Commit();
            _domainEventStorage.SaveShapShot(activeAccount);

            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));

            _repository.Add(activeAccount);
            _eventStoreUnitOfWork.Commit();

            var snapShot = _domainEventStorage.GetSnapShot(activeAccount.Id);

            Assert.IsNotNull(snapShot);
            Assert.IsInstanceOfType<ActiveAccountMemento>(snapShot.Memento);
        }

        [TestMethod]
        public void When_calling_Save_after_more_than_9_events_after_the_last_snap_shot_a_new_snapshot_will_be_created_10_events_after_last_snapshot_9_events_after_last_snapshot_verify_all_event_counts()
        {
            var activeAccount = ActiveAccount.CreateNew(Guid.NewGuid(), "AccountName", "Account Number");
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));

            _repository.Add(activeAccount);
            _eventStoreUnitOfWork.Commit();
            _domainEventStorage.SaveShapShot(activeAccount);

            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(1));

            _repository.Add(activeAccount);
            _eventStoreUnitOfWork.Commit();


            Assert.AreEqual(9, _domainEventStorage.GetEventsSinceLastSnapShot(activeAccount.Id).Count());
            Assert.AreEqual(19, _domainEventStorage.GetAllEvents(activeAccount.Id).Count());
        }

        [TestMethod]
        [ExpectedException(typeof(AccountBalanceToLowException))]
        public void When_calling_GetById_after_9_events_a_new_ActiveAcount_will_be_populated()
        {
            var activeAccount = ActiveAccount.CreateNew(Guid.NewGuid(), "AccountName", "Account Number");
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(2));
            activeAccount.Deposit(new Amount(3));
            activeAccount.Deposit(new Amount(4));
            activeAccount.Deposit(new Amount(5));
            activeAccount.Deposit(new Amount(6));
            activeAccount.Deposit(new Amount(7));
            activeAccount.Deposit(new Amount(8));

            _repository.Add(activeAccount);
            _eventStoreUnitOfWork.Commit();

            var sut = _repository.GetById<ActiveAccount>(activeAccount.Id);

            try
            {
                sut.Withdrawal(new Amount(36));
            }
            catch (Exception Ex)
            {
                Assert.Fail(string.Format("This should not fail: {0}", Ex.Message));
            }

            sut.Withdrawal(new Amount(1));
        }

        [TestMethod]
        [ExpectedException(typeof(AccountBalanceToLowException))]
        public void When_calling_GetById_after_every_10_events_a_new_snap_shot_will_be_created()
        {
            var activeAccount = ActiveAccount.CreateNew(Guid.NewGuid(), "AccountName", "Account Number");
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(2));
            activeAccount.Deposit(new Amount(3));
            activeAccount.Deposit(new Amount(4));
            activeAccount.Deposit(new Amount(5));
            activeAccount.Deposit(new Amount(6));
            activeAccount.Deposit(new Amount(7));
            activeAccount.Deposit(new Amount(8));
            activeAccount.Deposit(new Amount(9));

            _repository.Add(activeAccount);
            _eventStoreUnitOfWork.Commit();

            var sut = _repository.GetById<ActiveAccount>(activeAccount.Id);

            try
            {
                sut.Withdrawal(new Amount(45));
            }
            catch (Exception Ex)
            {
                Assert.Fail(string.Format("This should not fail: {0}", Ex.Message));
            }

            sut.Withdrawal(new Amount(1));
        }

        [TestMethod]
        [ExpectedException(typeof(AccountBalanceToLowException))]
        public void When_calling_GetById_after_every_10_events_a_new_snap_shot_will_be_created_11_events()
        {
            var activeAccount = ActiveAccount.CreateNew(Guid.NewGuid(), "AccountName", "Account Number");
            activeAccount.Deposit(new Amount(1));
            activeAccount.Deposit(new Amount(2));
            activeAccount.Deposit(new Amount(3));
            activeAccount.Deposit(new Amount(4));
            activeAccount.Deposit(new Amount(5));
            activeAccount.Deposit(new Amount(6));
            activeAccount.Deposit(new Amount(7));
            activeAccount.Deposit(new Amount(8));
            activeAccount.Deposit(new Amount(9));
            activeAccount.Deposit(new Amount(10));

            _repository.Add(activeAccount);
            _eventStoreUnitOfWork.Commit();

            var sut = _repository.GetById<ActiveAccount>(activeAccount.Id);

            try
            {
                sut.Withdrawal(new Amount(55));
            }
            catch (Exception Ex)
            {
                Assert.Fail(string.Format("This should not fail: {0}", Ex.Message));
            }

            sut.Withdrawal(new Amount(1));
        }
    }
}