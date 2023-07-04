using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.EventStore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using Test.Fohjin.DDD.TestUtilities;
using Test.Fohjin.DDD.TestUtilities.Tools;

namespace Test.Fohjin.DDD.Commands
{
    [TestClass]
    public class All_commands_must_have_a_handler : ContextualTestClassBase
    {
        [ContextualTestMethod]
        [DynamicData(nameof(TestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(TestDataDisplayName))]
        public async Task TestCommandHandlers(Type commandType, Type handlerType)
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
            if (command != null)
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
}