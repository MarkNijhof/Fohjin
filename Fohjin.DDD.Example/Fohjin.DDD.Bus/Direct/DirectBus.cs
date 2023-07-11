using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Fohjin.DDD.Bus.Direct
{
    public class DirectBus : IBus
    {
        private IRouteMessages? _routeMessages;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _log;

        private readonly ConcurrentQueue<object> _preCommitQueue = new();
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
            _preCommitQueue.Enqueue(message);
        }

        public void Publish(IEnumerable<object> messages)
        {
            _log.LogInformation($"{nameof(Publish)}: {{{nameof(messages)}}}", messages);
            foreach (var message in messages)
                _preCommitQueue.Enqueue(message);
        }

        public async Task CommitAsync()
        {
            _log.LogInformation($"{nameof(CommitAsync)}");

            while (_preCommitQueue.TryDequeue(out var @obj))
            {
                await _postCommitQueue.PutAsync(@obj);
            }
        }

        public void Rollback()
        {
            _log.LogInformation($"{nameof(Rollback)}");
                _preCommitQueue.Clear();
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