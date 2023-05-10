using Fohjin.DDD.EventStore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Fohjin.DDD.EventStore
{
    public static class ServiceCollectionExtensions
    {
        public static T AddEventStoreServices<T>(this T service) where T : IServiceCollection
        {
            service.TryAddTransient(typeof(IDomainRepository<>), typeof(DomainRepository<>));
            service.TryAddTransient(typeof(IEventStoreUnitOfWork<>), typeof(EventStoreUnitOfWork<>));
            service.TryAddTransient(typeof(IIdentityMap<>), typeof(EventStoreIdentityMap<>));

            service.TryAddTransient<IUnitOfWork, EventStoreUnitOfWork<IDomainEvent>>();
            return service;
        }
    }
}