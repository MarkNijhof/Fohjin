using Fohjin.DDD.Bus.Direct;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Fohjin.DDD.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static T AddConfigurationServices<T>(this T service) where T : IServiceCollection
        {
            service.TryAddTransient<ICommandHandlerHelper, CommandHandlerHelper>();

            //private const string sqLiteConnectionString = "Data Source=domainDataBase.db3";

            //public DomainRegistry()
            //{


            //    ForRequestedType<IDomainEventStorage<IDomainEvent>>()
            //        .TheDefault.Is.OfConcreteType<DomainEventStorage<IDomainEvent>>()
            //        .WithCtorArg("sqLiteConnectionString").EqualTo(sqLiteConnectionString);

            //    ForRequestedType<IIdentityMap<IDomainEvent>>()
            //        .TheDefault.Is.OfConcreteType<EventStoreIdentityMap<IDomainEvent>>();

            //    ForRequestedType<IEventStoreUnitOfWork<IDomainEvent>>()
            //        .CacheBy(InstanceScope.Hybrid)
            //        .TheDefault.Is.OfConcreteType<EventStoreUnitOfWork<IDomainEvent>>();

            //    ForRequestedType<IUnitOfWork>()
            //        .TheDefault.Is.ConstructedBy(x => x.GetInstance<IEventStoreUnitOfWork<IDomainEvent>>());

            //    ForRequestedType<IDomainRepository<IDomainEvent>>()
            //        .TheDefault.Is.OfConcreteType<DomainRepository<IDomainEvent>>();
            //}

            return service;
        }
    }
}