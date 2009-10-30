using System;
using Fohjin.DDD.Domain.Mementos;

namespace Fohjin.DDD.EventStore.SQLite
{
    [Serializable]
    public class SnapShot : ISnapShot
    {
        public SnapShot(Guid eventProviderId, int version, IMemento memento)
        {
            EventProviderId = eventProviderId;
            Version = version;
            Memento = memento;
        }

        public Guid EventProviderId { get; private set; }
        public int Version { get; private set; }
        public IMemento Memento { get; private set; }
    }
}