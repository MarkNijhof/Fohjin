using System;
using System.Collections.Generic;

namespace Fohjin.EventStore.Infrastructure
{
    public interface ICacheRegisteredEvents
    {
        void Add(Type type, Dictionary<Type, List<Action<object>>> cache);
        Dictionary<Type, List<Action<object>>> Get(Type type);
    }

    public class RegisteredEventsCache : ICacheRegisteredEvents
    {
        private readonly Dictionary<Type, Dictionary<Type, List<Action<object>>>> _cache;

        public RegisteredEventsCache()
        {
            _cache = new Dictionary<Type, Dictionary<Type, List<Action<object>>>>();
        }

        public void Add(Type type, Dictionary<Type, List<Action<object>>> cache)
        {
            _cache.Add(type, cache);
        }

        public Dictionary<Type, List<Action<object>>> Get(Type type)
        {
            Dictionary<Type, List<Action<object>>> cache;
            _cache.TryGetValue(type, out cache);
            return cache;
        }
    }
}