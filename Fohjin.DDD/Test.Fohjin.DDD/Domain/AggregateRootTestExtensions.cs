using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.Events;
using NUnit.Framework;

namespace Test.Fohjin.DDD.Domain
{
    public class ThenAttribute : TestAttribute { }
    public class SpecificationAttribute : TestFixtureAttribute { }

    public static class AggregateRootTestExtensions
    {
        public static IDomainEvent Number(this IEnumerable<IDomainEvent> events, int value)
        {
            return events.ToList()[--value];
        }
        public static void CountIs(this IEnumerable<IDomainEvent> events, int value)
        {
            Assert.AreEqual(value, events.ToList().Count());
        }
        public static void WillBeOfType<TType>(this object theEvent)
        {
            Assert.AreEqual(typeof(TType), theEvent.GetType());
        }
        public static void WillBe(this object source, object value)
        {
            Assert.AreEqual(value, source);
        }
        public static void WillNotBe(this object source, object value)
        {
            Assert.AreNotEqual(value, source);
        }
        public static void WithMessage(this Exception theException, string message)
        {
            Assert.AreEqual(message, theException.Message);
        }
        public static TDomainEvent Last<TDomainEvent>(this IEnumerable<IDomainEvent> events)
        {
            return (TDomainEvent) events.Last();
        }
        public static TDomainEvent LastMinus<TDomainEvent>(this IEnumerable<IDomainEvent> events, int minus)
        {
            return (TDomainEvent) events.ToList()[events.Count() - minus];
        }
    }
}