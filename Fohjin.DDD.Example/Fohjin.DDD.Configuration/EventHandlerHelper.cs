using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events;

namespace Fohjin.DDD.Configuration
{
    public class EventHandlerHelper
    {
        public static IDictionary<Type, IList<Type>> GetEventHandlers()
        {
            var commands = new Dictionary<Type, IList<Type>>();
            typeof(ClientCreatedEventHandler)
                .Assembly
                .GetExportedTypes()
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IEventHandler<>)))
                .ToList()
                .ForEach(x => AddItem(commands, x));
            return commands;
        }

        public static IEnumerable<Type> GetEvents()
        {
            return typeof(DomainEvent)
                .Assembly
                .GetExportedTypes()
                .Where(x => x.BaseType == typeof(DomainEvent))
                .ToList();
        }

        private static void AddItem(IDictionary<Type, IList<Type>> dictionary, Type type)
        {
            var theEvent = type.GetInterfaces()
                .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEventHandler<>))
                .First()
                .GetGenericArguments()
                .First();

            if (!dictionary.ContainsKey(theEvent))
                dictionary.Add(theEvent, new List<Type>());

            dictionary[theEvent].Add(type);
        }
    }
}