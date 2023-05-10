using Fohjin.DDD.Bus.Direct;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Fohjin.DDD.Bus
{
    public static class ServiceCollectionExtensions
    {
        public static T AddBusServices<T>(this T service) where T : IServiceCollection
        {
            service.TryAddSingleton<IRouteMessages, MessageRouter>();
            service.TryAddSingleton<IBus>(sp=> ActivatorUtilities.CreateInstance<DirectBus>(sp));
            service.TryAddSingleton<IQueue, InMemoryQueue>();
            return service;
        }
    }
}