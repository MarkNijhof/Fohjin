using Fohjin.DDD.EventStore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Fohjin.DDD.EventStore
{
    public static class ServiceCollectionExtensions
    {
        public static T AddEventStoreServices<T>(this T service) where T : IServiceCollection
        {
            service.TryAddSingleton(typeof(IDomainRepository<>), typeof(DomainRepository<>));
            service.TryAddSingleton(typeof(IEventStoreUnitOfWork<>), typeof(EventStoreUnitOfWork<>));
            service.TryAddSingleton(typeof(IIdentityMap<>), typeof(EventStoreIdentityMap<>));

            service.TryAddSingleton<IUnitOfWork>(sp => sp.GetRequiredService<IEventStoreUnitOfWork<IDomainEvent>>());
            return service;
        }
    }
}