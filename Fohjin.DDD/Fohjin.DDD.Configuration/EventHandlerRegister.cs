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
            foreach (var command in events)
            {
                if (!eventHandlers.ContainsKey(command))
                {
                    stringBuilder.AppendLine(string.Format("No event handler found for event '{0}'", command.FullName));
                    continue;
                }

                ForRequestedType(typeof(IEventHandler<>).MakeGenericType(command))
                    .TheDefaultIsConcreteType(eventHandlers[command]);
            }
            if (stringBuilder.Length > 0)
                throw new Exception(string.Format("\n\nEvent handler exceptions:\n{0}\n", stringBuilder));
        }

        private static IDictionary<Type, Type> GetEventHandlers()
        {
            IDictionary<Type, Type> commands = new Dictionary<Type, Type>();
            typeof(IEventHandler<>)
                .Assembly
                .GetExportedTypes()
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IEventHandler<>)))
                .ToList()
                .ForEach(x => AddItem(commands, x));
            return commands;
        }

        private static void AddItem(ICollection<KeyValuePair<Type, Type>> dictionary, Type type)
        {
            dictionary
                .Add(new KeyValuePair<Type, Type>(
                    type.GetInterfaces()
                        .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEventHandler<>))
                        .First()
                        .GetGenericArguments()
                        .First(),
                    type));
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