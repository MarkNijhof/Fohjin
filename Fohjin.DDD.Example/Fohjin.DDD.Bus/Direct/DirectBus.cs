using Microsoft.Extensions.DependencyInjection;

namespace Fohjin.DDD.Bus.Direct
{
    public class DirectBus : IBus
    {
        private IRouteMessages _routeMessages;
        private readonly IServiceProvider _serviceProvider;

        private readonly object _lockObject = new();
        private readonly Queue<object> _preCommitQueue = new(32);
        private readonly InMemoryQueue _postCommitQueue = new();

        public DirectBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _postCommitQueue.PopAsync(DoPublishAsync).GetAwaiter().GetResult();
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

        private async Task DoPublishAsync(object message)
        {
            _routeMessages ??= _serviceProvider.GetRequiredService<IRouteMessages>();
            try
            {
                await _routeMessages.RouteAsync(message);
            }
            finally
            {
                await _postCommitQueue.PopAsync(DoPublishAsync);
            }
        }
    }
}