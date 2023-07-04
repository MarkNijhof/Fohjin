using Fohjin.DDD.BankApplication;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Common;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.Domain.Mementos;
using Fohjin.DDD.EventStore;
using Fohjin.DDD.EventStore.SQLite;
using Fohjin.DDD.EventStore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Test.Fohjin.DDD.TestUtilities;

namespace Test.Fohjin.DDD.Domain.Repositories
{
    [TestClass]
    public class clientRepositoryTest
    {
        private readonly IServiceCollection _services = new ServiceCollection()
            .AddLogging(opt => opt.AddConsole().SetMinimumLevel(LogLevel.Information))
            ;
        public IServiceCollection Services => _services;

        private IServiceProvider _provider;
        public IServiceProvider Provider => _provider ??= _services.BuildServiceProvider();

        public ILogger<T> Logger<T>() => Provider.GetRequiredService<ILogger<T>>();

        public TestContext TestContext { get; set; } = null!;

        private IDomainRepository<IDomainEvent> _repository;
        private DomainEventStorage<IDomainEvent> _domainEventStorage;
        private EventStoreIdentityMap<IDomainEvent> _eventStoreIdentityMap;
        private EventStoreUnitOfWork<IDomainEvent> _eventStoreUnitOfWork;

        [TestInitialize]
        public void SetUp()
        {
            TestContext.SetupWorkingDirectory();
            var dataBaseFile = Path.Combine(
                (string)TestContext.Properties[TestContextExtensions.TestWorkingDirectory]
                ?? throw new NotSupportedException($"TestContext property is missing {nameof(TestContextExtensions.TestWorkingDirectory)}"),
                DomainDatabaseBootStrapper.DataBaseFile
                );

            new DomainDatabaseBootStrapper().ReCreateDatabaseSchema(dataBaseFile);

            var sqliteConnectionString = string.Format("Data Source={0}", dataBaseFile);

            var config = new ConfigurationBuilder()
                .AddTupleConfiguration((DomainEventStorage.ConnectionStringConfigKey, sqliteConnectionString))
                .Build();

            _domainEventStorage = new DomainEventStorage<IDomainEvent>(
                config,
                new ExtendedFormatter()
                );

            _eventStoreIdentityMap = new EventStoreIdentityMap<IDomainEvent>();
            _eventStoreUnitOfWork = new EventStoreUnitOfWork<IDomainEvent>(
                _domainEventStorage,
                _eventStoreIdentityMap,
                new Mock<IBus>().Object,
                Logger<EventStoreUnitOfWork<IDomainEvent>>()
                );
            _repository = new DomainRepository<IDomainEvent>(
                _eventStoreUnitOfWork,
                _eventStoreIdentityMap,
                Logger<DomainRepository<IDomainEvent>>()
                );
        }

        [TestMethod]
        public void When_calling_Save_it_will_add_the_domain_events_to_the_domain_event_storage()
        {
            var client = Client.CreateNew(new ClientName("New Client"), new Address("Street", "123", "5000", "Bergen"), new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));

            _repository?.Add(client);
            _eventStoreUnitOfWork?.Commit();

            Assert.AreEqual(3, _domainEventStorage?.GetEventsSinceLastSnapShot(client.Id).Count());
            Assert.AreEqual(3, _domainEventStorage?.GetAllEvents(client.Id).Count());
        }

        [TestMethod]
        public void When_calling_Save_it_will_reset_the_domain_events()
        {
            var client = Client.CreateNew(new ClientName("New Client"), new Address("Street", "123", "5000", "Bergen"), new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));

            _repository?.Add(client);
            _eventStoreUnitOfWork?.Commit();

            var clientForRepository = (IEventProvider<IDomainEvent>)client;

            Assert.AreEqual(0, clientForRepository.GetChanges().Count());
        }

        [TestMethod]
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

            _repository?.Add(client);
            _eventStoreUnitOfWork?.Commit();

            Assert.IsNull(_domainEventStorage?.GetSnapShot(client.Id));
        }

        [TestMethod]
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

            _repository?.Add(client);
            _eventStoreUnitOfWork?.Commit();
            _domainEventStorage?.SaveShapShot(client);

            var snapShot = _domainEventStorage?.GetSnapShot(client.Id);

            Assert.IsNotNull(snapShot);
            Assert.IsInstanceOfType<ClientMemento>(snapShot.Memento);
        }

        [TestMethod]
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

            _repository?.Add(client);
            _eventStoreUnitOfWork?.Commit();
            _domainEventStorage?.SaveShapShot(client);

            var snapShot = _domainEventStorage?.GetSnapShot(client.Id);

            Assert.IsNotNull(snapShot);
            Assert.IsInstanceOfType<ClientMemento>(snapShot.Memento);
        }

        [TestMethod]
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

            _repository?.Add(client);
            _eventStoreUnitOfWork?.Commit();
            _domainEventStorage?.SaveShapShot(client);

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

            _repository?.Add(client);
            _eventStoreUnitOfWork?.Commit();

            var snapShot = _domainEventStorage?.GetSnapShot(client.Id);

            Assert.IsNotNull(snapShot);
            Assert.IsInstanceOfType<ClientMemento>(snapShot.Memento);
        }

        [TestMethod]
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

            _repository?.Add(client);
            _eventStoreUnitOfWork?.Commit();
            _domainEventStorage?.SaveShapShot(client);

            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));

            _repository?.Add(client);
            _eventStoreUnitOfWork?.Commit();

            var snapShot = _domainEventStorage?.GetSnapShot(client.Id);

            Assert.IsNotNull(snapShot);
            Assert.IsInstanceOfType<ClientMemento>(snapShot.Memento);
        }

        [TestMethod]
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

            _repository?.Add(client);
            _eventStoreUnitOfWork?.Commit();
            _domainEventStorage?.SaveShapShot(client);

            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));
            client.UpdatePhoneNumber(new PhoneNumber("1234567890"));

            _repository?.Add(client);
            _eventStoreUnitOfWork?.Commit();

            Assert.AreEqual(9, _domainEventStorage?.GetEventsSinceLastSnapShot(client.Id).Count());
            Assert.AreEqual(19, _domainEventStorage?.GetAllEvents(client.Id).Count());
        }

        [TestMethod]
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

            _repository?.Add(client);

            _repository?.GetById<Client>(client.Id);
        }

        [TestMethod]
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

            _repository?.Add(client);
            _eventStoreUnitOfWork?.Commit();

            _repository?.GetById<Client>(client.Id);
        }

        [TestMethod]
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

            _repository?.Add(client);
            _eventStoreUnitOfWork?.Commit();

            _repository?.GetById<Client>(client.Id);
        }
    }
}