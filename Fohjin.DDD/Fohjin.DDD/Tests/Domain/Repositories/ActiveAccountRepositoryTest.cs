using System;
using System.Linq;
using Fohjin.DDD.Domain;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.Repositories;
using Fohjin.DDD.Domain.ValueObjects;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Fohjin.DDD.Tests.Domain.Repositories
{
    [TestFixture]
    public class ActiveAccountRepositoryTest
    {
        private ActiveAccountRepository _activeAccountRepository;
        private DomainEventStorage _domainEventStorage;
        private SnapShotStorage _snapShotStorage;

        [SetUp]
        public void SetUp()
        {
            _domainEventStorage = new DomainEventStorage();
            _snapShotStorage = new SnapShotStorage();
            _activeAccountRepository = new ActiveAccountRepository(_domainEventStorage, _snapShotStorage);
        }

        [Test]
        public void When_calling_Save_it_will_add_the_domain_events_to_the_domain_event_storage()
        {
            IActiveAccount activeAccount = new ActiveAccount();
            activeAccount.Create();
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));

            _activeAccountRepository.Save(activeAccount);

            Assert.That(_domainEventStorage.GetEvents().Count(), Is.EqualTo(3));
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
        public void When_calling_Save_after_every_10_events_a_new_snap_shot_will_be_created_9_events_will_not()
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

            Assert.That(_snapShotStorage.HasSnapShots(), Is.False);
        }

        [Test]
        public void When_calling_Save_after_every_10_events_a_new_snap_shot_will_be_created()
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

            Assert.That(_snapShotStorage.HasSnapShots(), Is.True);
            Assert.That(_snapShotStorage.GetLastSnapShot().Key, Is.EqualTo(10));
            Assert.That(_snapShotStorage.GetLastSnapShot().Value, Is.InstanceOfType(typeof(ActiveAccountMemento)));
        }

        [Test]
        public void When_calling_Save_after_every_10_events_a_new_snap_shot_will_be_created_11_events()
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

            _activeAccountRepository.Save(activeAccount);

            Assert.That(_snapShotStorage.HasSnapShots(), Is.True);
            Assert.That(_snapShotStorage.GetLastSnapShot().Key, Is.EqualTo(10));
            Assert.That(_snapShotStorage.GetLastSnapShot().Value, Is.InstanceOfType(typeof(ActiveAccountMemento)));
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void When_calling_GetById_after_9_events_a_new_ActiveAcount_will_be_populated()
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

            var sut = _activeAccountRepository.GetById(new Guid());

            try
            {
                sut.Withdrawl(new Amount(8));
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
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));
            activeAccount.Deposite(new Amount(1));

            _activeAccountRepository.Save(activeAccount);

            var sut = _activeAccountRepository.GetById(new Guid());

            try
            {
                sut.Withdrawl(new Amount(9));
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

            var sut = _activeAccountRepository.GetById(new Guid());

            try
            {
                sut.Withdrawl(new Amount(10));
            }
            catch (Exception Ex)
            {
                Assert.Fail(string.Format("This should not fail: {0}", Ex.Message));
            }

            sut.Withdrawl(new Amount(1));
        }
    }
}