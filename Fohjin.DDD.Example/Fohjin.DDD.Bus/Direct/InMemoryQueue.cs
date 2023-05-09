namespace Fohjin.DDD.Bus.Direct
{
    public class InMemoryQueue : IQueue
    {
        private readonly Queue<object> _itemQueue = new(32);
        private readonly Queue<Func<object, Task>> _listenerQueue = new(32);

        public void Put(object item)
        {
            if (_listenerQueue.Count == 0)
            {
                _itemQueue.Enqueue(item);
                return;
            }

            var listener = _listenerQueue.Dequeue();
            listener(item);
        }

        public async Task PopAsync(Func<object, Task> popAction)
        {
            if (_itemQueue.Count == 0)
            {
                _listenerQueue.Enqueue(popAction);
                return;
            }

            var item = _itemQueue.Dequeue();
            popAction(item);
        }
    }
}