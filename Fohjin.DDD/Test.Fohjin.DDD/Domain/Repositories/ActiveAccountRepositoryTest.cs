using System;
using System.Data.SQLite;
using System.Linq;
using Fohjin.DDD.Configuration;
using Fohjin.DDD.Domain;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.Repositories;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.DDD.Infrastructure;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Test.Fohjin.DDD.Domain.Repositories
{
    [TestFixture]
    public class ActiveAccountRepositoryTest
    {
        private const string dataBaseFile = "domainDataBase.db3";

        private ActiveAccountRepository _activeAccountRepository;
        private DomainEventStorage _domainEventStorage;
        private SnapShotStorage _snapShotStorage;

        [SetUp]
        public void SetUp()
        {
            new DatabaseBootStrapper().ReCreateDatabaseSchema();

            var sqLiteConnection = new SQLiteConnection(string.Format("Data Source={0}", dataBaseFile));
            _domainEventStorage = new DomainEventStorage(sqLiteConnection, new Serializer());
            _snapShotStorage = new SnapShotStorage(sqLiteConnection, new Serializer(), _domainEventStorage);
            _activeAccountRepository = new ActiveAccountRepository(_domainEventStorage, _snapShotStorage, new EventPropagator());
        }

        [Test]
        public void When_calling_Save_it_will_add_the_domain_events_to_the_domain_event_storage()
        {
            IActiveAccount activeAccount = new ActiveAccount();
            activeAccount.Create();
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));

            _activeAccountRepository.Save(activeAccount);

            Assert.That(_domainEventStorage.GetEventsSinceLastSnapShot(activeAccount.Id).Count(), Is.EqualTo(0));
            Assert.That(_domainEventStorage.GetAllEvents(activeAccount.Id).Count(), Is.EqualTo(3));
        }

        [Test]
        public void When_calling_Save_it_will_reset_the_domain_events()
        {
            IActiveAccount activeAccount = new ActiveAccount();
            activeAccount.Create();
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));

            _activeAccountRepository.Save(activeAccount);

            var activeAccountForRepository = (IExposeMyInternalChanges)activeAccount;

            Assert.That(activeAccountForRepository.GetChanges().Count(), Is.EqualTo(0));
        }

        [Test]
        public void When_calling_Save_after_more_than_9_events_a_new_snap_shot_will_be_created_9_events_will_not()
        {
            IActiveAccount activeAccount = new ActiveAccount();
            activeAccount.Create();
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));

            _activeAccountRepository.Save(activeAccount);

            Assert.That(_snapShotStorage.GetLastSnapShot(activeAccount.Id), Is.Null);
        }

        [Test]
        public void When_calling_Save_after_more_than_9_events_a_new_snap_shot_will_be_created_10_events()
        {
            IActiveAccount activeAccount = new ActiveAccount();
            activeAccount.Create();
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));

            var activeAccountForRepository = (IExposeMyInternalChanges)activeAccount;
            var id = activeAccountForRepository.GetChanges().Last().Id;

            _activeAccountRepository.Save(activeAccount);

            var snapShot = _snapShotStorage.GetLastSnapShot(activeAccount.Id);

            Assert.That(snapShot, Is.Not.Null);
            Assert.That(snapShot.EventLocation, Is.EqualTo(id));
            Assert.That(snapShot.Memento, Is.InstanceOfType(typeof(ActiveAccountMemento)));
        }

        [Test]
        public void When_calling_Save_after_more_than_9_events_a_new_snap_shot_will_be_created_11_events()
        {
            IActiveAccount activeAccount = new ActiveAccount();
            activeAccount.Create();
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));

            var activeAccountForRepository = (IExposeMyInternalChanges)activeAccount;
            var id = activeAccountForRepository.GetChanges().Last().Id;

            _activeAccountRepository.Save(activeAccount);

            var snapShot = _snapShotStorage.GetLastSnapShot(activeAccount.Id);

            Assert.That(snapShot, Is.Not.Null);
            Assert.That(snapShot.EventLocation, Is.EqualTo(id));
            Assert.That(snapShot.Memento, Is.InstanceOfType(typeof(ActiveAccountMemento)));
        }

        [Test]
        public void When_calling_Save_after_more_than_9_events_after_the_last_snap_shot_a_new_snapshot_will_be_created_10_events_after_last_snapshot()
        {
            IActiveAccount activeAccount = new ActiveAccount();
            activeAccount.Create();
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));

            _activeAccountRepository.Save(activeAccount);

            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));

            var activeAccountForRepository = (IExposeMyInternalChanges)activeAccount;
            var id = activeAccountForRepository.GetChanges().Last().Id;

            _activeAccountRepository.Save(activeAccount);

            var snapShot = _snapShotStorage.GetLastSnapShot(activeAccount.Id);

            Assert.That(snapShot, Is.Not.Null);
            Assert.That(snapShot.EventLocation, Is.EqualTo(id));
            Assert.That(snapShot.Memento, Is.InstanceOfType(typeof(ActiveAccountMemento)));
        }

        [Test]
        public void When_calling_Save_after_more_than_9_events_after_the_last_snap_shot_a_new_snapshot_will_be_created_10_events_after_last_snapshot_9_events_after_last_snapshot()
        {
            IActiveAccount activeAccount = new ActiveAccount();
            activeAccount.Create();
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));

            var activeAccountForRepository = (IExposeMyInternalChanges)activeAccount;
            var id = activeAccountForRepository.GetChanges().Last().Id;

            _activeAccountRepository.Save(activeAccount);

            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));

            _activeAccountRepository.Save(activeAccount);

            var snapShot = _snapShotStorage.GetLastSnapShot(activeAccount.Id);

            Assert.That(snapShot, Is.Not.Null);
            Assert.That(snapShot.EventLocation, Is.EqualTo(id));
            Assert.That(snapShot.Memento, Is.InstanceOfType(typeof(ActiveAccountMemento)));
        }

        [Test]
        public void When_calling_Save_after_more_than_9_events_after_the_last_snap_shot_a_new_snapshot_will_be_created_10_events_after_last_snapshot_9_events_after_last_snapshot_verify_all_event_counts()
        {
            IActiveAccount activeAccount = new ActiveAccount();
            activeAccount.Create();
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));

            _activeAccountRepository.Save(activeAccount);

            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));

            _activeAccountRepository.Save(activeAccount);

            Assert.That(_domainEventStorage.GetEventsSinceLastSnapShot(activeAccount.Id).Count(), Is.EqualTo(9));
            Assert.That(_domainEventStorage.GetAllEvents(activeAccount.Id).Count(), Is.EqualTo(19));
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void When_calling_GetById_after_9_events_a_new_ActiveAcount_will_be_populated()
        {
            IActiveAccount activeAccount = new ActiveAccount();
            activeAccount.Create();
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(2));
            activeAccount.Deposite(new Amount(3));
            activeAccount.Deposite(new Amount(4));
            activeAccount.Deposite(new Amount(5));
            activeAccount.Deposite(new Amount(6));
            activeAccount.Deposite(new Amount(7));
            activeAccount.Deposite(new Amount(8));

            _activeAccountRepository.Save(activeAccount);

            var sut = _activeAccountRepository.GetById(activeAccount.Id);

            try
            {
                sut.Withdrawl(new Amount(36));
            }
            catch (Exception Ex)
            {
                Assert.Fail(string.Format("This should not fail: {0}", Ex.Message));
            }

            sut.Withdrawl(new Amount(1));
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void When_calling_GetById_after_every_10_events_a_new_snap_shot_will_be_created()
        {
            IActiveAccount activeAccount = new ActiveAccount();
            activeAccount.Create();
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(2));
            activeAccount.Deposite(new Amount(3));
            activeAccount.Deposite(new Amount(4));
            activeAccount.Deposite(new Amount(5));
            activeAccount.Deposite(new Amount(6));
            activeAccount.Deposite(new Amount(7));
            activeAccount.Deposite(new Amount(8));
            activeAccount.Deposite(new Amount(9));

            _activeAccountRepository.Save(activeAccount);

            var sut = _activeAccountRepository.GetById(activeAccount.Id);

            try
            {
                sut.Withdrawl(new Amount(45));
            }
            catch (Exception Ex)
            {
                Assert.Fail(string.Format("This should not fail: {0}", Ex.Message));
            }

            sut.Withdrawl(new Amount(1));
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void When_calling_GetById_after_every_10_events_a_new_snap_shot_will_be_created_11_events()
        {
            IActiveAccount activeAccount = new ActiveAccount();
            activeAccount.Create();
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(2));
            activeAccount.Deposite(new Amount(3));
            activeAccount.Deposite(new Amount(4));
            activeAccount.Deposite(new Amount(5));
            activeAccount.Deposite(new Amount(6));
            activeAccount.Deposite(new Amount(7));
            activeAccount.Deposite(new Amount(8));
            activeAccount.Deposite(new Amount(9));
            activeAccount.Deposite(new Amount(10));

            _activeAccountRepository.Save(activeAccount);

            var sut = _activeAccountRepository.GetById(activeAccount.Id);

            try
            {
                sut.Withdrawl(new Amount(55));
            }
            catch (Exception Ex)
            {
                Assert.Fail(string.Format("This should not fail: {0}", Ex.Message));
            }

            sut.Withdrawl(new Amount(1));
        }
    }
}