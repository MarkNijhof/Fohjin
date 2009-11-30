using System;
using System.Collections.Generic;

namespace Fohjin.EventStore.Configuration
{
    public class ApprovedEntitiesCache
    {
        private readonly List<Type> _cache;

        public ApprovedEntitiesCache()
        {
            _cache = new List<Type>();
        }

        public void RegisterApprovedEntity(Type entityType)
        {
            _cache.Add(entityType);
        }

        public bool IsEntityApproved(Type entityType)
        {
            return _cache.Contains(entityType);
        }
    }
}