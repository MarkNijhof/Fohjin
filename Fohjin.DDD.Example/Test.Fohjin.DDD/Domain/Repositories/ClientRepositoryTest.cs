using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Configuration;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.Domain.Mementos;
using Fohjin.DDD.EventStore;
using Fohjin.DDD.EventStore.SQLite;
using Fohjin.DDD.EventStore.Storage;
using Moq;
using NUnit.Framework;


namespace Test.Fohjin.DDD.Domain.Repositories
{
    [TestFixture]
    public class clientRepositoryTest
    {
        private const string dataBaseFile = "domainDataBase.db3";

        private IDomainRepository<IDomainEvent> _repository;
        private DomainEventStorage<IDomainEvent> _domainEventStorage;
        private EventStoreIdentityMap<IDomainEvent> _eventStoreIdentityMap;
        private EventStoreUnitOfWork<IDomainEvent> _eventStoreUnitOfWork;

        [SetUp]
        public void SetUp()
        {
            new DomainDatabaseBootStrapper().ReCreateDatabaseSchema();

            var sqliteConnectionString = string.Format("Data Source={0}", dataBaseFile);

            _domainEventStorage = new DomainEventStorage<IDomainEvent>(sqliteConnectionString, new BinaryFormatter());
            _eventStoreIdentityMap = new EventStoreIdentityMap<IDomainEvent>();
            _eventStoreUnitOfWork = new EventStoreUnitOfWork<IDomainEvent>(_domainEventStorage, _eventStoreIdentityMap, new Mock<IBus>().Object);
            _repository = new DomainRepository<IDomainEvent>(_eventStoreUnitOfWork, _eventStoreIdentityMap);
        }

        [Test]
        public void When_calling_Save_it_will_add_the_domain_events_to_the_domain_event_storage()
        {
            var client = Client.CreateNew(new ClientName("New Client"), new Address("Street", "123", "5000", "Bergen"), new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));

            _repository.Add(client);
            _eventStoreUnitOfWork.Commit();

            Assert.That(_domainEventStorage.GetEventsSinceLastSnapShot(client.Id).Count(), Is.EqualTo(3));
            Assert.That(_domainEventStorage.GetAllEvents(client.Id).Count(), Is.EqualTo(3));
        }

        [Test]
        public void When_calling_Save_it_will_reset_the_domain_events()
        {
            var client = Client.CreateNew(new ClientName("New Client"), new Address("Street", "123", "5000", "Bergen"), new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));

            _repository.Add(client);
            _eventStoreUnitOfWork.Commit();

            var clientForRepository = (IEventProvider<IDomainEvent>)client;

            Assert.That(clientForRepository.GetChanges().Count(), Is.EqualTo(0));
        }

        [Test]
        public void When_calling_Save_after_more_than_9_events_a_new_snap_shot_will_be_created_9_events_will_not()
        {
            var client = Client.CreateNew(new ClientName("New Client"), new Address("Street", "123", "5000", "Bergen"), new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));

            _repository.Add(client);
            _eventStoreUnitOfWork.Commit();

            Assert.That(_domainEventStorage.GetSnapShot(client.Id), Is.Null);
        }

        [Test]
        public void When_calling_Save_after_more_than_9_events_a_new_snap_shot_will_be_created_10_events()
        {
            var client = Client.CreateNew(new ClientName("New Client"), new Address("Street", "123", "5000", "Bergen"), new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));

            _repository.Add(client);
            _eventStoreUnitOfWork.Commit();
            _domainEventStorage.SaveShapShot(client);

            var snapShot = _domainEventStorage.GetSnapShot(client.Id);

            Assert.That(snapShot, Is.Not.Null);
            Assert.That(snapShot.Memento, Is.InstanceOfType(typeof(ClientMemento)));
        }

        [Test]
        public void When_calling_Save_after_more_than_9_events_a_new_snap_shot_will_be_created_11_events()
        {
            var client = Client.CreateNew(new ClientName("New Client"), new Address("Street", "123", "5000", "Bergen"), new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));

            _repository.Add(client);
            _eventStoreUnitOfWork.Commit();
            _domainEventStorage.SaveShapShot(client);

            var snapShot = _domainEventStorage.GetSnapShot(client.Id);

            Assert.That(snapShot, Is.Not.Null);
            Assert.That(snapShot.Memento, Is.InstanceOfType(typeof(ClientMemento)));
        }

        [Test]
        public void When_calling_Save_after_more_than_9_events_after_the_last_snap_shot_a_new_snapshot_will_be_created_10_events_after_last_snapshot()
        {
            var client = Client.CreateNew(new ClientName("New Client"), new Address("Street", "123", "5000", "Bergen"), new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));

            _repository.Add(client);
            _eventStoreUnitOfWork.Commit();
            _domainEventStorage.SaveShapShot(client);

            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));

            _repository.Add(client);
            _eventStoreUnitOfWork.Commit();

            var snapShot = _domainEventStorage.GetSnapShot(client.Id);

            Assert.That(snapShot, Is.Not.Null);
            Assert.That(snapShot.Memento, Is.InstanceOfType(typeof(ClientMemento)));
        }

        [Test]
        public void When_calling_Save_after_more_than_9_events_after_the_last_snap_shot_a_new_snapshot_will_be_created_10_events_after_last_snapshot_9_events_after_last_snapshot()
        {
            var client = Client.CreateNew(new ClientName("New Client"), new Address("Street", "123", "5000", "Bergen"), new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));

            _repository.Add(client);
            _eventStoreUnitOfWork.Commit();
            _domainEventStorage.SaveShapShot(client);

            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));

            _repository.Add(client);
            _eventStoreUnitOfWork.Commit();

            var snapShot = _domainEventStorage.GetSnapShot(client.Id);

            Assert.That(snapShot, Is.Not.Null);
            Assert.That(snapShot.Memento, Is.InstanceOfType(typeof(ClientMemento)));
        }

        [Test]
        public void When_calling_Save_after_more_than_9_events_after_the_last_snap_shot_a_new_snapshot_will_be_created_10_events_after_last_snapshot_9_events_after_last_snapshot_verify_all_event_counts()
        {
            var client = Client.CreateNew(new ClientName("New Client"), new Address("Street", "123", "5000", "Bergen"), new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));

            _repository.Add(client);
            _eventStoreUnitOfWork.Commit();
            _domainEventStorage.SaveShapShot(client);

            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));

            _repository.Add(client);
            _eventStoreUnitOfWork.Commit();

            Assert.That(_domainEventStorage.GetEventsSinceLastSnapShot(client.Id).Count(), Is.EqualTo(9));
            Assert.That(_domainEventStorage.GetAllEvents(client.Id).Count(), Is.EqualTo(19));
        }

        [Test]
        public void When_calling_GetById_after_9_events_a_new_Client_will_be_populated()
        {
            var client = Client.CreateNew(new ClientName("New Client"), new Address("Street", "123", "5000", "Bergen"), new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("0987654321"));

            _repository.Add(client);

            _repository.GetById<Client>(client.Id);
        }

        [Test]
        public void When_calling_GetById_after_every_10_events_a_new_snap_shot_will_be_created()
        {
            var client = Client.CreateNew(new ClientName("New Client"), new Address("Street", "123", "5000", "Bergen"), new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("0987654321"));

            _repository.Add(client);
            _eventStoreUnitOfWork.Commit();

            _repository.GetById<Client>(client.Id);
        }

        [Test]
        public void When_calling_GetById_after_every_10_events_a_new_snap_shot_will_be_created_11_events()
        {
            var client = Client.CreateNew(new ClientName("New Client"), new Address("Street", "123", "5000", "Bergen"), new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("0987654321"));

            _repository.Add(client);
            _eventStoreUnitOfWork.Commit();

            _repository.GetById<Client>(client.Id);
        }
    }
}