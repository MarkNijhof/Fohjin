using Fohjin.DDD.EventStore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Fohjin.DDD.EventStore.SQLite
{
    public static class ServiceCollectionExtensions
    {
        public static T AddEventStoreSqliteServices<T>(this T service) where T : IServiceCollection
        {
            service.AddTransient(typeof(IDomainEventStorage<>), typeof(DomainEventStorage<>));
            return service;
        }
    }
}