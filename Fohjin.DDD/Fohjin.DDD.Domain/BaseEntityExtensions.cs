using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.Domain
{
    public static class TryGetByIdExtension
    {
        public static bool TryGetValueById<TEventProvider>(this IEnumerable<TEventProvider> list, Guid Id, out IEventProvider baseEntity) where TEventProvider : IEventProvider
        {
            baseEntity = list.Where(x => x.Id == Id).FirstOrDefault();
            return baseEntity != null;
        }
    }
}