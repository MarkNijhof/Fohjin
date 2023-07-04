using Fohjin.DDD.Bus.Direct;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Queueing;

[TestClass]
public class InMemoryQueue_test
{
    private readonly IServiceCollection _services = new ServiceCollection()
        .AddLogging(opt => opt.AddConsole().SetMinimumLevel(LogLevel.Information))
        ;
    public IServiceCollection Services => _services;

    private IServiceProvider _provider;
    public IServiceProvider Provider => _provider ??= _services.BuildServiceProvider();

    private ILogger<InMemoryQueue> _logger;
    public ILogger<InMemoryQueue> Logger =>
        _logger ??= Provider.GetRequiredService<ILogger<InMemoryQueue>>();

    [TestMethod]
    public async Task When_adding_items_to_the_queue_they_can_later_be_retrieved_from_the_queue()
    {
        var firstItem = "not set";
        var secondItem = "not set";

        var inMemoryQueue = new InMemoryQueue(this.Logger);

        await inMemoryQueue.PutAsync("first item");
        await inMemoryQueue.PutAsync("second item");

        Assert.AreEqual("not set", firstItem);
        Assert.AreEqual("not set", secondItem);

        await inMemoryQueue.PopAsync(x => Task.FromResult(firstItem = x.ToString()));
        await inMemoryQueue.PopAsync(x => Task.FromResult(secondItem = x.ToString()));

        Assert.AreEqual("first item", firstItem);
        Assert.AreEqual("second item", secondItem);
    }

    [TestMethod]
    public async Task When_adding_listeners_to_the_queue_they_can_later_be_executed_with_new_items_from_the_queue()
    {
        var firstItem = "not set";
        var secondItem = "not set";

        var inMemoryQueue = new InMemoryQueue(this.Logger);

        await inMemoryQueue.PopAsync(x => Task.FromResult(firstItem = x.ToString()));
        await inMemoryQueue.PopAsync(x => Task.FromResult(secondItem = x.ToString()));

        Assert.AreEqual("not set", firstItem);
        Assert.AreEqual("not set", secondItem);

        await inMemoryQueue.PutAsync("first item");
        await inMemoryQueue.PutAsync("second item");

        Assert.AreEqual("first item", firstItem);
        Assert.AreEqual("second item", secondItem);
    }
}