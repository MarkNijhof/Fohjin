using System.Collections.Generic;
using Fohjin.DDD.Domain.Memento;

namespace Fohjin.DDD.Domain.Repositories
{
    public interface ISnapShotStorage
    {
        void Add(int eventLocation, IMemento memento);
        bool HasSnapShots();
        KeyValuePair<int, IMemento> GetLastSnapShot();
    }
}