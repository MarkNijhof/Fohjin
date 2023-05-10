using Microsoft.Extensions.Logging;

namespace Fohjin.DDD.Common
{
    public class SystemTimer : ISystemTimer, IDisposable
    {
        private readonly List<Task> _timers = new();
        private readonly ILogger _log;

        public SystemTimer(
            ILogger<SystemTimer> log)
        {
            _log = log;
        }

        public void Dispose() => 
            Task.WaitAll(_timers.ToArray());

        public void Trigger(Action value, int @in)
        {
            _log.LogInformation($"Schedule Timer: {value} ({@in})");
            _timers.Add(Task.Run(async () =>
            {
                await Task.Delay(@in);
                _log.LogInformation($"Triggered Timer: {value} ({@in})");
                value();
            }));
        }
    }
}