using System.Collections.Generic;

namespace Fohjin.DDD.Bus.Direct
{
    public class DirectBus : IBus
    {
        private readonly IRouteMessages _routeMessages;
        private readonly object _lockObject = new object();
        private readonly Queue<object> _preCommitQueue;
        private readonly InMemoryQueue _postCommitQueue;

        public DirectBus(IRouteMessages routeMessages)
        {
            _routeMessages = routeMessages;
            _preCommitQueue = new Queue<object>(32);
            _postCommitQueue = new InMemoryQueue();
            _postCommitQueue.Pop(DoPublish);
        }

        public void Publish(object message)
        {
            lock (_lockObject)
            {
                _preCommitQueue.Enqueue(message);
            }
        }

        public void Publish(IEnumerable<object> messages)
        {
            lock (_lockObject)
            {
                foreach (var message in messages)
                {
                    _preCommitQueue.Enqueue(message);
                }
            }
        }

        public void Commit()
        {
            lock (_lockObject)
            {
                while (_preCommitQueue.Count > 0)
                {
                    _postCommitQueue.Put(_preCommitQueue.Dequeue());
                }
            }
        }

        public void Rollback()
        {
            lock (_lockObject)
            {
                _preCommitQueue.Clear();
            }
        }

        private void DoPublish(object message)
        {
            try
            {
                _routeMessages.Route(message);
            }
            finally
            {
                _postCommitQueue.Pop(DoPublish);
            }
        }
    }
}