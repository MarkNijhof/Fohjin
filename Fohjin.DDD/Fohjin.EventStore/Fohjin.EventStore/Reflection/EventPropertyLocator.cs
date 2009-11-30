using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fohjin.EventStore.Reflection
{
    public class EventPropertyLocator
    {
        public IEnumerable<PropertyInfo> RetrieveDomainEventProperties(Type registeredEvent)
        {
            return registeredEvent
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => !GetBaseInterfaceProperties().Contains(x.Name));
        }

        private static List<string> GetBaseInterfaceProperties()
        {
            return typeof(IDomainEvent).GetProperties().Select(x => x.Name).ToList();            
        }
    }
}