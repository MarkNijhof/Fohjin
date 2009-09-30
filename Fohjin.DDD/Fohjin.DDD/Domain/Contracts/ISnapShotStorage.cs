using System;

namespace Fohjin.DDD.Domain.Contracts
{
    public interface ISnapShotStorage
    {
        ISnapShot GetLastSnapShot(Guid entityId);
        void SaveShapShot(IExposeMyInternalChanges entity);
    }
}