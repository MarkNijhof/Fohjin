using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.EventStore;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using Test.Fohjin.DDD.TestUtilities;
using Test.Fohjin.DDD.TestUtilities.Tools;

namespace Test.Fohjin.DDD.Events;

[TestClass]
public class All_domain_events_must_have_a_handler
{
    public TestContext TestContext { get; set; } = null!;

    [DataTestMethod]
    [DynamicData(nameof(TestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(TestDataDisplayName))]
    public async Task TestEventHandler(Type eventType, Type handlerType = null)
    {
        this.TestContext.WriteLine($"RUN_ID:{TestContext.Properties[$"RUN_ID"] = Guid.NewGuid()}");
        this.TestContext.Properties[$"Parameter::{nameof(eventType)}"] = eventType;
        this.TestContext.Properties[$"Parameter::{nameof(handlerType)}"] = handlerType;

        if (handlerType == null && (eventType.Namespace?.Contains("Test") ?? true))
        {
            Assert.Inconclusive("No handlers exist but it's a test event anyway");
        }

        Assert.IsNotNull(handlerType, "No handlers exist");

        var services = new ServiceCollection()
            .AddLogging(log => log.AddConsole().SetMinimumLevel(LogLevel.Information))
            .AddSingleton(_ => TestContext)
            .AddSingleton(typeof(IDomainRepository<>), typeof(TestDomainRepository<>))
            .AddSingleton<IReportingRepository, TestReportingRepository>()
            .AddSingleton<ISendMoneyTransfer, TestSendMoneyTransfer>()
            ;
        var serviceProvider = services.BuildServiceProvider();

        if (eventType.GetNonDefaultValue(serviceProvider) is IDomainEvent evnt && ActivatorUtilities.CreateInstance(serviceProvider, handlerType) is IEventHandler instance)
            await instance.ExecuteAsync(evnt);
    }
    public static string TestDataDisplayName(MethodInfo methodInfo, object[] data) =>
        $"{methodInfo.Name} for {((Type)data[0]).Name} => {((Type?)data?[1])?.Name}";

    public static IEnumerable<object[]> TestData()
    {
        var commands = from eventType in typeof(IDomainEvent).GetInstanceTypes()
                       let handlerInterfaceType = typeof(IEventHandler<>).MakeGenericType(eventType)
                       let handlers = handlerInterfaceType.GetInstanceTypes()
                       from handlerType in handlers.DefaultIfEmpty()
                       select new
                       {
                           eventType,
                           handlerType,
                       };

        var items = commands
            ;
        var mapped = items.Select(i => new object[] { i.eventType, i.handlerType });
        return mapped;
    }
}