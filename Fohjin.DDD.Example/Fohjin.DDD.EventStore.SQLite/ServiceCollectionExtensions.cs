using Fohjin.DDD.EventStore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Fohjin.DDD.EventStore.SQLite
{
    public static class ServiceCollectionExtensions
    {
        public static T AddEventStoreSqliteServices<T>(this T service) where T : IServiceCollection
        {
            service.TryAddSingleton(typeof(IDomainEventStorage<>), typeof(DomainEventStorage<>));
            return service;
        }
    }
}