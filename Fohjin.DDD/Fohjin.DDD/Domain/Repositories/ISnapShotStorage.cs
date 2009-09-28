using System;

namespace Fohjin.DDD.Domain.Repositories
{
    public interface ISnapShotStorage
    {
        bool HasSnapShots();
        ISnapShot GetLastSnapShot();
        void Add(Guid id, ISnapShot snapShot);
    }
}