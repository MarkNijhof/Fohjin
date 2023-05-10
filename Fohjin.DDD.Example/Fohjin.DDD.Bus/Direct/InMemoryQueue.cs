using Microsoft.Extensions.Logging;

namespace Fohjin.DDD.Bus.Direct
{
    public class InMemoryQueue : IQueue
    {
        private static int _seed;
        private readonly int _id = _seed++;


        private readonly Queue<object> _itemQueue = new(32);
        private readonly Queue<Func<object, Task>> _listenerQueue = new(32);

        private readonly ILogger _log;

        public InMemoryQueue(
            ILogger<InMemoryQueue> log
            )
        {
            _log = log;
        }

        public async Task PutAsync(object item)
        {
            _log.LogInformation($"PutAsync> {{{nameof(item)}}}", item);
            if (!_listenerQueue.Any())
            {
                _itemQueue.Enqueue(item);
                return;
            }

            var listener = _listenerQueue.Dequeue();
            await listener(item);
        }

        public async Task PopAsync(Func<object, Task> popAction)
        {
            _log.LogInformation($"PopAsync({_id})> {{{nameof(popAction)}}}", popAction);
            if (!_itemQueue.Any())
            {
                _listenerQueue.Enqueue(popAction);
                return;
            }

            var item = _itemQueue.Dequeue();
            await popAction(item);
        }
    }
}