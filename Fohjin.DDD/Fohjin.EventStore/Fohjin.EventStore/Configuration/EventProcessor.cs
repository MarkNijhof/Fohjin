using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fohjin.EventStore.Configuration
{
    public class EventProcessor
    {
        public Type RegisteredEvent { get; private set; }
        public PropertyInfo Property { get; private set; }
        public Action<object, Dictionary<string, object>> ProcessorEventProperty { get; private set; }

        public EventProcessor(Type registeredEvent, PropertyInfo property, Action<object, Dictionary<string, object>> eventPropertyProcessor)
        {
            RegisteredEvent = registeredEvent;
            Property = property;
            ProcessorEventProperty = eventPropertyProcessor;
        }
    }
}