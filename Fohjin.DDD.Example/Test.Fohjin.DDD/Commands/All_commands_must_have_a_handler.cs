using Fohjin.DDD.Configuration;
using Fohjin.DDD.EventStore.Storage.Memento;
using Fohjin.DDD.EventStore.Storage;
using Fohjin.DDD.EventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Text;
using Test.Fohjin.DDD.TestUtilities;
using Fohjin.DDD.Commands;
using Fohjin.DDD.CommandHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Fohjin.DDD.Events;

namespace Test.Fohjin.DDD.Commands
{
    [TestClass]
    public class All_commands_must_have_a_handler
    {
        public TestContext TestContext { get; set; }


        [DataTestMethod]
        [DynamicData(nameof(TestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(TestDataDisplayName))]
        public async Task TestEventHandler(Type commandType, Type? handlerType)
        {
            Assert.IsNotNull(handlerType, "No handlers exist");

            var services = new ServiceCollection()
                .AddLogging(log => log.AddConsole().SetMinimumLevel(LogLevel.Information))
                .AddSingleton(_ => TestContext)
                .AddSingleton(typeof(IDomainRepository<>), typeof(TestDomainRepository<>))
                ;
            var serviceProvider = services.BuildServiceProvider();

            var command = (ICommand)commandType.GetNonDefaultValue(serviceProvider);

            var instance = (ICommandHandler)ActivatorUtilities.CreateInstance(serviceProvider, handlerType);
            await instance.ExecuteAsync(command);

        }
        public static string TestDataDisplayName(MethodInfo methodInfo, object[] data) =>
            $"{methodInfo.Name} for {((Type)data[0]).Name} => {((Type)data[1]).Name}";

        public static IEnumerable<object[]> TestData()
        {
            var commands = from commandType in typeof(ICommand).GetInstanceTypes()
                           let handlerInterfaceType = typeof(ICommandHandler<>).MakeGenericType(commandType)
                           let handlers = handlerInterfaceType.GetInstanceTypes()
                           from handlerType in handlers.DefaultIfEmpty()
                           select new
                           {
                               commandType,
                               handlerType,
                           };

            var items = commands
                ;
            var mapped = items.Select(i => new object[] { i.commandType, i.handlerType });
            return mapped;
        }
    }

    public class TestDomainRepository<TDomainEvent> : IDomainRepository<TDomainEvent>
         where TDomainEvent : IDomainEvent
    {
        private readonly TestContext _testContext;
        private readonly IServiceProvider _serviceProvider;

        public TestDomainRepository(
            TestContext testContext,
            IServiceProvider serviceProvider
            )
        {
            _testContext = testContext;
            _serviceProvider = serviceProvider;
        }

        void IDomainRepository<TDomainEvent>.Add<TAggregate>(TAggregate aggregateRoot)
        {
            _testContext.AddResults(typeof(TAggregate).Name, aggregateRoot);
        }

        TAggregate IDomainRepository<TDomainEvent>.GetById<TAggregate>(Guid id)
        {
            var aggregate = (TAggregate)typeof(TAggregate).FillObject(_serviceProvider);
            aggregate.Id = id;
            return aggregate;
        }
    }
}