using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fohjin.DDD.Bus.Direct
{
    public class DirectBus : IBus
    {
        private IRouteMessages _routeMessages;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _log;

        private readonly object _lockObject = new();
        private readonly Queue<object> _preCommitQueue = new(32);
        private readonly IQueue _postCommitQueue;

        public DirectBus(
            IServiceProvider serviceProvider,
            IQueue postCommitQueue,
            ILogger<DirectBus> log

            )
        {
            _serviceProvider = serviceProvider;
            _log = log;
            _postCommitQueue = postCommitQueue;
            _postCommitQueue.PopAsync(DoPublishAsync).GetAwaiter().GetResult();
        }

        public void Publish(object message)
        {
            _log.LogInformation($"{nameof(Publish)}: {{{nameof(message)}}}", message);
            lock (_lockObject)
            {
                _preCommitQueue.Enqueue(message);
            }
        }

        public void Publish(IEnumerable<object> messages)
        {
            _log.LogInformation($"{nameof(Publish)}: {{{nameof(messages)}}}", messages);
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
            _log.LogInformation($"{nameof(Commit)}");
            lock (_lockObject)
            {
                while (_preCommitQueue.Any())
                {
                    _postCommitQueue.PutAsync(_preCommitQueue.Dequeue()).GetAwaiter().GetResult();
                }
            }
        }

        public void Rollback()
        {
            _log.LogInformation($"{nameof(Rollback)}");
            lock (_lockObject)
            {
                _preCommitQueue.Clear();
            }
        }

        private async Task DoPublishAsync(object message)
        {
            _log.LogInformation($"{nameof(DoPublishAsync)}: {{{nameof(message)}}}", message);
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