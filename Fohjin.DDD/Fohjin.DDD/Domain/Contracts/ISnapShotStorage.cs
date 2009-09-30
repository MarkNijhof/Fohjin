using System;

namespace Fohjin.DDD.Domain.Contracts
{
    public interface ISnapShotStorage
    {
        ISnapShot GetLastSnapShot(Guid entityId);
        void Add(Guid entityId, ISnapShot snapShot);
        void MakeShapShot(IExposeMyInternalChanges entity);
    }
}