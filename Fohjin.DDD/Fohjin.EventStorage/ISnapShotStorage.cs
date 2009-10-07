using System;
using Fohjin.DDD.Domain;

namespace Fohjin.EventStorage
{
    public interface ISnapShotStorage
    {
        ISnapShot GetSnapShot(Guid entityId);
        void SaveShapShot(IEventProvider entity);
    }
}