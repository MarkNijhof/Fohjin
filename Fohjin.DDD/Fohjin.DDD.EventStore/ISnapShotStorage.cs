using System;
using Fohjin.DDD.Domain;

namespace Fohjin.DDD.EventStore
{
    public interface ISnapShotStorage
    {
        ISnapShot GetSnapShot(Guid entityId);
        void SaveShapShot(IEventProvider entity);
    }
}