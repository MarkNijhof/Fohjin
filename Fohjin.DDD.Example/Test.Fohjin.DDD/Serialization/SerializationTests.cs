using Fohjin.DDD.Commands;
using Fohjin.DDD.EventStore;
using Fohjin.DDD.EventStore.Storage;
using Fohjin.DDD.EventStore.Storage.Memento;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using Test.Fohjin.DDD.TestUtilities;

namespace Test.Fohjin.DDD.Serialization;

[TestClass]
public class SerializationTests
{
    public TestContext TestContext { get; set; }= null!;


    [DataTestMethod]
    [DynamicData(nameof(TestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(TestDataDisplayName))]
    public void ModelPersistenceTest(Type type, Type interfaceType)
    {
        var obj = type.BuildObject();
        TestContext
            .AddResults(type.Name, obj)
            .GetResults(type.Name, type, out var result)
            .AddResults(type.Name + "_back", result)
            ;
        type.EnsureNotDefault(obj);
        Assert.IsNotNull(result);
    }
    public static string TestDataDisplayName(MethodInfo methodInfo, object[] data) =>
        $"{methodInfo.Name} :: {((Type)data[1]).Name} for {((Type)data[0]).Name}";

    public static IEnumerable<object[]> TestData()
    {
        var commands = typeof(ICommand).GetInstanceTypes().Select(t => new { inf = typeof(ICommand), type = t });
        var domainEvents = typeof(IDomainEvent).GetInstanceTypes().Select(t => new { inf = typeof(IDomainEvent), type = t });
        var mementos = typeof(IMemento).GetInstanceTypes().Select(t => new { inf = typeof(IMemento), type = t });
        var snapShots = typeof(ISnapShot).GetInstanceTypes().Select(t => new { inf = typeof(ISnapShot), type = t });

        var items = commands
            .Concat(domainEvents)
            .Concat(mementos)
            .Concat(snapShots)
            ;
        var mapped = items.Select(i => new object[] { i.type, i.inf });
        return mapped;
    }
}
