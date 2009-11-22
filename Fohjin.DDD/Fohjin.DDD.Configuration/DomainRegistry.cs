using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Bus.Direct;
using Fohjin.DDD.EventStore;
using Fohjin.DDD.EventStore.SQLite;
using Fohjin.DDD.EventStore.Storage;
using StructureMap.Attributes;
using StructureMap.Configuration.DSL;
using IUnitOfWork = Fohjin.DDD.EventStore.IUnitOfWork;

namespace Fohjin.DDD.Configuration
{
    public class DomainRegistry : Registry
    {
        private const string sqLiteConnectionString = "Data Source=domainDataBase.db3";

        public DomainRegistry()
        {
            ForRequestedType<IBus>()
                .CacheBy(InstanceScope.Hybrid)
                .TheDefault.Is.OfConcreteType<DirectBus>();
            
            ForRequestedType<IRouteMessages>()
                .AsSingletons()
                .TheDefault.Is.OfConcreteType<MessageRouter>();

            ForRequestedType<IFormatter>()
                .TheDefault.Is.ConstructedBy(x => new BinaryFormatter());
            
            ForRequestedType<IDomainEventStorage<IDomainEvent>>()
                .TheDefault.Is.OfConcreteType<DomainEventStorage<IDomainEvent>>()
                .WithCtorArg("sqLiteConnectionString").EqualTo(sqLiteConnectionString);

            ForRequestedType<IIdentityMap<IDomainEvent>>()
                .TheDefault.Is.OfConcreteType<EventStoreIdentityMap<IDomainEvent>>();

            ForRequestedType<IEventStoreUnitOfWork<IDomainEvent>>()
                .CacheBy(InstanceScope.Hybrid)
                .TheDefault.Is.OfConcreteType<EventStoreUnitOfWork<IDomainEvent>>();
            
            ForRequestedType<IUnitOfWork>()
                .TheDefault.Is.ConstructedBy(x => x.GetInstance<IEventStoreUnitOfWork<IDomainEvent>>());

            ForRequestedType<IDomainRepository<IDomainEvent>>()
                .TheDefault.Is.OfConcreteType<DomainRepository<IDomainEvent>>();
        }
    }
}