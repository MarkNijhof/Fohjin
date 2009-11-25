using System;
using System.Collections.Generic;

namespace Fohjin.EventStore.Infrastructure
{
    public interface ICacheRegisteredEvents
    {
        bool TryGetValue(Type type, out Dictionary<Type, List<Action<object, object>>> cache);
        void Add(Type type, Dictionary<Type, List<Action<object, object>>> cache);
    }

    public class RegisteredEventsCache : ICacheRegisteredEvents
    {
        private Dictionary<Type, Dictionary<Type, List<Action<object, object>>>> _cache;

        public RegisteredEventsCache()
        {
            _cache = new Dictionary<Type, Dictionary<Type, List<Action<object, object>>>>();
        }

        public bool TryGetValue(Type type, out Dictionary<Type, List<Action<object, object>>> cache)
        {
            return _cache.TryGetValue(type, out cache);
        }

        public void Add(Type type, Dictionary<Type, List<Action<object, object>>> cache)
        {
            _cache.Add(type, cache);
        }
    }
}