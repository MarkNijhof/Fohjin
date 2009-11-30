using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fohjin.EventStore.Reflection
{
    public class DomainEventLocator
    {
        public IEnumerable<Type> RetrieveDomainEvents(Type entityType)
        {
            return (IEnumerable<Type>) RegiteredEventsMethodSelector(entityType).Invoke(null, new object[] {});
        }

        public bool HasRequiredMethod(Type entityType)
        {
            var regiteredEventsMethod = RegiteredEventsMethodSelector(entityType);
            return regiteredEventsMethod != null && regiteredEventsMethod.ToString() == "System.Collections.Generic.IEnumerable`1[System.Type] RegisteredEvents()";
        }

        private static MethodInfo RegiteredEventsMethodSelector(IReflect entityType)
        {
            return entityType.GetMethod("RegisteredEvents", BindingFlags.Static | BindingFlags.NonPublic);
        }
    }
}