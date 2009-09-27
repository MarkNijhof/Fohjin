using System.Linq;
using System.Collections.Generic;
using Fohjin.DDD.Domain.Memento;

namespace Fohjin.DDD.Domain.Repositories
{
    public class SnapShotStorage : ISnapShotStorage 
    {
        private readonly Dictionary<int, IMemento> _snapShots;

        public SnapShotStorage()
        {
            _snapShots = new Dictionary<int, IMemento>();
        }

        public void Add(int eventLocation, IMemento memento)
        {
            _snapShots.Add(eventLocation, memento);
        }

        public bool HasSnapShots()
        {
            return _snapShots.Count > 0;
        }

        public KeyValuePair<int, IMemento> GetLastSnapShot()
        {
            return _snapShots.Last();
        }
    }
}