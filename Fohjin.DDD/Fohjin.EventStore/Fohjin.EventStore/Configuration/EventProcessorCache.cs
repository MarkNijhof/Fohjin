using System;
using System.Collections.Generic;
using System.Linq;

namespace Fohjin.EventStore.Configuration
{
    public class EventProcessorCache
    {
        private Dictionary<Type, Dictionary<Type, IEnumerable<EventProcessor>>> _cache;

        public EventProcessorCache()
        {
            _cache = new Dictionary<Type, Dictionary<Type, IEnumerable<EventProcessor>>>();
        }

        public IEnumerable<Type> GetEventsFor(Type entityType)
        {
            if (!_cache.ContainsKey(entityType))
                return new List<Type>();

            return _cache[entityType].Keys.AsEnumerable();
        }

        public IEnumerable<EventProcessor> GetEventProcessorsFor(Type entityType, Type domainEvent)
        {
            if (!_cache.ContainsKey(entityType))
                return new List<EventProcessor>();

            if (!_cache[entityType].ContainsKey(domainEvent))
                return new List<EventProcessor>();

            return _cache[entityType][domainEvent];
        }

        public void RegisterEventProcessors(Type entityType, Type registeredEvent, IEnumerable<EventProcessor> eventProcessors)
        {
            Dictionary<Type, IEnumerable<EventProcessor>> events;
            if (!_cache.TryGetValue(entityType, out events))
            {
                events = new Dictionary<Type, IEnumerable<EventProcessor>>();
                events[registeredEvent] = eventProcessors;
                _cache.Add(entityType, events);
                return;
            }

            if (events.ContainsKey(registeredEvent))
                return;

            events[registeredEvent] = eventProcessors;
        }
    }
}