using Fohjin.DDD.Bus.Direct;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework.SyntaxHelpers;

namespace Test.Fohjin.DDD.Queueing
{
    [TestClass]
    public class InMemoryQueue_test
    {
        [TestMethod]
        public void When_adding_items_to_the_queue_they_can_later_be_retrieved_from_the_queue()
        {
            var firstItem = "not set";
            var secondItem = "not set";

            var inMemoryQueue = new InMemoryQueue();

            inMemoryQueue.Put("first item");
            inMemoryQueue.Put("second item");

            Assert.AreEqual(firstItem, Is.EqualTo("not set"));
            Assert.AreEqual(secondItem, Is.EqualTo("not set"));

            inMemoryQueue.Pop(x => firstItem = x.ToString());
            inMemoryQueue.Pop(x => secondItem = x.ToString());

            Assert.AreEqual(firstItem, Is.EqualTo("first item"));
            Assert.AreEqual(secondItem, Is.EqualTo("second item"));
        }

        [TestMethod]
        public void When_adding_listeners_to_the_queue_they_can_later_be_executed_with_new_items_from_the_queue()
        {
            var firstItem = "not set";
            var secondItem = "not set";

            var inMemoryQueue = new InMemoryQueue();

            inMemoryQueue.Pop(x => firstItem = x.ToString());
            inMemoryQueue.Pop(x => secondItem = x.ToString());

            Assert.AreEqual(firstItem, Is.EqualTo("not set"));
            Assert.AreEqual(secondItem, Is.EqualTo("not set"));

            inMemoryQueue.Put("first item");
            inMemoryQueue.Put("second item");

            Assert.AreEqual(firstItem, Is.EqualTo("first item"));
            Assert.AreEqual(secondItem, Is.EqualTo("second item"));
        }
    }
}