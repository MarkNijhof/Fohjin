using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events;
using StructureMap.Configuration.DSL;

namespace Fohjin.DDD.Configuration
{
    public class EventHandlerRegister : Registry
    {
        public EventHandlerRegister()
        {
            var events = GetEvents();
            var eventHandlers = GetEventHandlers();

            var stringBuilder = new StringBuilder();
            foreach (var theEvent in events)
            {
                if (!eventHandlers.ContainsKey(theEvent))
                {
                    stringBuilder.AppendLine(string.Format("No event handler found for event '{0}'", theEvent.FullName));
                    continue;
                }

                foreach (var eventHandler in eventHandlers[theEvent])
                {
                    ForRequestedType(typeof(IEventHandler<>).MakeGenericType(theEvent))
                        .TheDefaultIsConcreteType(eventHandler)
                        .WithName(eventHandler.Name);
                }
            }
            if (stringBuilder.Length > 0)
                throw new Exception(string.Format("\n\nEvent handler exceptions:\n{0}\n", stringBuilder));
        }

        private static IDictionary<Type, IList<Type>> GetEventHandlers()
        {
            var commands = new Dictionary<Type, IList<Type>>();
            typeof(IEventHandler<>)
                .Assembly
                .GetExportedTypes()
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IEventHandler<>)))
                .ToList()
                .ForEach(x => AddItem(commands, x));
            return commands;
        }

        private static void AddItem(IDictionary<Type, IList<Type>> dictionary, Type type)
        {
            var theEvent = type.GetInterfaces()
                .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof (IEventHandler<>))
                .First()
                .GetGenericArguments()
                .First();

            if (!dictionary.ContainsKey(theEvent))
                dictionary.Add(theEvent, new List<Type>());

            dictionary[theEvent].Add(type);
        }

        private static List<Type> GetEvents()
        {
            return typeof(DomainEvent)
                .Assembly
                .GetExportedTypes()
                .Where(x => x.BaseType == typeof(DomainEvent))
                .ToList();
        }
    }
}