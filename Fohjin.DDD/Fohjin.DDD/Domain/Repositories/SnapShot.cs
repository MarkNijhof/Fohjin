using System;
using Fohjin.DDD.Domain.Contracts;
using Fohjin.DDD.Domain.Memento;

namespace Fohjin.DDD.Domain.Repositories
{
    [Serializable]
    public class SnapShot : ISnapShot
    {
        public SnapShot(Guid eventLocation, IMemento memento)
        {
            EventLocation = eventLocation;
            Memento = memento;
        }

        public IMemento Memento { get; private set; }
        public Guid EventLocation { get; private set; }
    }
}