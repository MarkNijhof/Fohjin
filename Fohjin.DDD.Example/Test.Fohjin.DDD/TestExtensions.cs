using Fohjin.DDD.EventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD
{
    public static class TestExtensions
    {
        public static IDomainEvent? Number(this IEnumerable<IDomainEvent>? events, int value)
        {
            return events?.ToList()[--value];
        }
        public static void CountIs(this IEnumerable<IDomainEvent>? events, int value)
        {
            Assert.AreEqual(value, events?.ToList().Count());
        }
        public static void WillBeOfType<TType>(this object? theEvent)
        {
            Assert.AreEqual(typeof(TType), theEvent?.GetType());
        }
        public static void WillBe(this object? source, object? value)
        {
            Assert.AreEqual(value, source);
        }
        public static void WillNotBe(this object? source, object? value)
        {
            Assert.AreNotEqual(value, source);
        }
        public static void WillBeSimuliar(this object? source, object? value)
        {
            Assert.AreEqual(value?.ToString(), source?.ToString());
        }
        public static void WillNotBeSimuliar(this object? source, object? value)
        {
            Assert.AreNotEqual(value?.ToString(), source?.ToString());
        }
        public static void WithMessage(this Exception? theException, string message)
        {
            Assert.AreEqual(message, theException?.Message);
        }
        public static TDomainEvent? Last<TDomainEvent>(this IEnumerable<IDomainEvent>? events)
        {
            return (TDomainEvent?)events?.Last();
        }
        public static object? LastMinus(this IEnumerable<IDomainEvent>? events, int minus)
        {
            return events?.ToList()[events.Count() - 1 - minus];
        }
        public static TDomainEvent? LastMinus<TDomainEvent>(this IEnumerable<IDomainEvent>? events, int minus)
        {
            return (TDomainEvent?)events?.ToList()[events.Count() - 1 - minus];
        }
        public static TDomainEvent As<TDomainEvent>(this object theObject)
        {
            return (TDomainEvent)theObject;
        }
    }
}