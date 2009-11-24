using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;

namespace Test.Fohjin.EventStore
{
    public static class TestExtensions
    {
        public static void CountIs(this IEnumerable<object> objects, int value)
        {
            Assert.AreEqual(value, objects.ToList().Count());
        }
        public static void WillBeOfType<TType>(this object theObject)
        {
            Assert.AreEqual(typeof(TType), theObject.GetType());
        }
        public static void WillActLikeType<TType>(this object theObject)
        {
            Assert.IsTrue(theObject.GetType().BaseType == typeof(TType));
        }
        public static void WillImplementInterface<TInterfaceType>(this object theObject) where TInterfaceType : class
        {
            Assert.IsNotNull(theObject as TInterfaceType);
        }
        public static void WillBe(this object source, object value)
        {
            Assert.AreEqual(value, source);
        }
        public static void WillNotBe(this object source, object value)
        {
            Assert.AreNotEqual(value, source);
        }
        public static void WillBeSimuliar(this object source, object value)
        {
            Assert.AreEqual(value.ToString(), source.ToString());
        }
        public static void WillNotBeSimuliar(this object source, object value)
        {
            Assert.AreNotEqual(value.ToString(), source.ToString());
        }
        public static void WithMessage(this Exception theException, string message)
        {
            Assert.AreEqual(message, theException.Message);
        }
        public static TDomainEvent Last<TDomainEvent>(this IEnumerable<object> events)
        {
            return (TDomainEvent)events.Last();
        }
        public static object LastMinus(this IEnumerable<object> events, int minus)
        {
            return events.ToList()[events.Count() - 1 - minus];
        }
        public static TDomainEvent LastMinus<TDomainEvent>(this IEnumerable<object> events, int minus)
        {
            return (TDomainEvent)events.ToList()[events.Count() - 1 - minus];
        }
        public static TDomainEvent As<TDomainEvent>(this object theObject)
        {
            return (TDomainEvent)theObject;
        }
    }
}