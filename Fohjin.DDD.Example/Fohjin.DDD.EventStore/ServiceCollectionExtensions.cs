using Fohjin.DDD.EventStore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Fohjin.DDD.EventStore
{
    public static class ServiceCollectionExtensions
    {
        public static T AddEventStoreServices<T>(this T service) where T : IServiceCollection
        {
            service.AddTransient(typeof(IDomainRepository<>), typeof(DomainRepository<>));
            service.AddTransient(typeof(IEventStoreUnitOfWork<>), typeof(EventStoreUnitOfWork<>));
            service.AddTransient(typeof(IIdentityMap<>), typeof(EventStoreIdentityMap<>));
            return service;
        }
    }
}