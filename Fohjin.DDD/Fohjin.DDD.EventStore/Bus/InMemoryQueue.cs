using System;
using System.Collections.Generic;

namespace Fohjin.DDD.EventStore.Bus
{
    public interface IQueue
    {
        void Put(object item);
        void Pop(Action<object> popAction);
    }

    public class InMemoryQueue : IQueue
    {
        private readonly Queue<object> _queue;
        private readonly Queue<Action<object>> _listeners;

        public InMemoryQueue()
        {
            _queue = new Queue<object>(32);
            _listeners = new Queue<Action<object>>(32);
        }

        public void Put(object item)
        {
            if
                (_listeners.Count == 0)
            {
                _queue.Enqueue(item);
                return;
            }

            var listener = _listeners.Dequeue();
            listener(item);
        }

        public void Pop(Action<object> popAction)
        {
            if (_queue.Count == 0)
            {
                _listeners.Enqueue(popAction);
                return;
            }

            var item = _queue.Dequeue();
            popAction(item);
        }
    }
}