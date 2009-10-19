using System;
using System.Linq;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events;
using StructureMap.Configuration.DSL;

namespace Fohjin.DDD.Configuration
{
    public class EventHandlerRegister : Registry
    {
        public EventHandlerRegister()
        {
            var eventTypes = typeof(DomainEvent).Assembly.GetExportedTypes().Where(x => x.BaseType == typeof(DomainEvent)).ToList();
            var handlerTypes = typeof(IEventHandler<>).Assembly.GetExportedTypes().Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IEventHandler<>))).ToList();

            foreach (var type in eventTypes)
            {
                var eventName = type.Name;
                var handlerType = handlerTypes.SingleOrDefault(x => x.Name == string.Format("{0}Handler", eventName));

                if (handlerType == null)
                    throw new Exception(string.Format("No event handler found for event '{0}' expected '{1}.{2}Handler'", type.FullName, typeof(IEventHandler<>).Namespace, eventName));

                ForRequestedType(typeof (IEventHandler<>).MakeGenericType(type))
                    .TheDefaultIsConcreteType(handlerType);
            }
        }
    }
}