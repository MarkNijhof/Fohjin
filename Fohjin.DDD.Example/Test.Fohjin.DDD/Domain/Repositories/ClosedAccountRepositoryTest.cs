using Fohjin.DDD.BankApplication;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Common;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.EventStore;
using Fohjin.DDD.EventStore.SQLite;
using Fohjin.DDD.EventStore.Storage;
using Fohjin.DDD.EventStore.Storage.Memento;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Reflection;
using Test.Fohjin.DDD.TestUtilities;

namespace Test.Fohjin.DDD.Domain.Repositories;

[TestClass]
public class ClosedAccountRepositoryTest
{
    public TestContext TestContext { get; set; } = null!;

    private readonly IServiceCollection _services = new ServiceCollection()
        .AddLogging(opt => opt.AddConsole().SetMinimumLevel(LogLevel.Information))
        ;
    public IServiceCollection Services => _services;

    private IServiceProvider _provider;
    public IServiceProvider Provider => _provider ??= _services.BuildServiceProvider();

    public ILogger<T> Logger<T>() => Provider.GetRequiredService<ILogger<T>>();

    private IDomainRepository<IDomainEvent> _repository;
    private DomainEventStorage<IDomainEvent> _domainEventStorage;
    private EventStoreIdentityMap<IDomainEvent> _eventStoreIdentityMap;
    private EventStoreUnitOfWork<IDomainEvent> _eventStoreUnitOfWork;
    private List<Ledger> _ledgers;

    [TestInitialize]
    public void SetUp()
    {
        TestContext.SetupWorkingDirectory();
        var dataBaseFile = Path.Combine(
            (string)TestContext.Properties[TestContextExtensions.TestWorkingDirectory] ??
                throw new NotSupportedException($"TestContext.Properties is missing [TestContextExtensions.TestWorkingDirectory]"),
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
        _ledgers = new List<Ledger>
        {
            new CreditMutation(1, new AccountNumber("0987654321")),
            new DebitMutation(1, new AccountNumber("0987654321")),
            new CreditTransfer(1, new AccountNumber("0987654321")),
            new DebitTransfer(1, new AccountNumber("0987654321")),
            new DebitTransferFailed(1, new AccountNumber("0987654321")),
        };

        var closedAccount = ClosedAccount.CreateNew(Guid.NewGuid(), Guid.NewGuid(), _ledgers, new AccountName("AccountName"), new AccountNumber("1234567890"));

        _repository?.Add(closedAccount);
        _eventStoreUnitOfWork?.Commit();

        Assert.AreEqual(1, _domainEventStorage?.GetEventsSinceLastSnapShot(closedAccount.Id).Count());
        Assert.AreEqual(1, _domainEventStorage?.GetAllEvents(closedAccount.Id).Count());
    }

    [TestMethod]
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

        _repository?.Add(closedAccount);
        _eventStoreUnitOfWork?.Commit();

        var closedAccountForRepository = (IEventProvider<IDomainEvent>)closedAccount;

        Assert.AreEqual(0, closedAccountForRepository.GetChanges().Count());
    }

    [TestMethod]
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

        var memento = ((IOriginator)closedAccount).CreateMemento();

        var newClosedAccount = new ClosedAccount();

        ((IOriginator)newClosedAccount).SetMemento(memento);

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
                var ledgers = field.GetValue(recreated) as List<Ledger>;
                foreach (var ledger in field.GetValue(original) as List<Ledger> ?? Enumerable.Empty<Ledger>())
                {
                    Assert.AreEqual(ledgers?[counter++].ToString(), ledger.ToString());
                }
                continue;
            }
            Assert.AreEqual(field.GetValue(recreated)?.ToString(), field.GetValue(original)?.ToString());
        }
    }


}