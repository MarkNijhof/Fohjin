using System.Data.Entity.Core.Metadata.Edm;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Bus.Direct;
using Fohjin.DDD.EventStore;
using Fohjin.DDD.EventStore.SQLite;
using Fohjin.DDD.EventStore.Storage;
using StructureMap.Attributes;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;
using IUnitOfWork = Fohjin.DDD.EventStore.IUnitOfWork;

namespace Fohjin.DDD.Configuration
{
    public class DomainRegistry : Registry
    {
        private const string sqLiteConnectionString = "Data Source=domainDataBase.db3";

        public DomainRegistry()
        {
            For<IBus>()
                .LifecycleIs(new UniquePerRequestLifecycle())
                .Use<DirectBus>();

            For<IRouteMessages>()
                .Singleton()
                .Use<MessageRouter>();

            For<IFormatter>()
                .Use(x => new BinaryFormatter());

            For<IDomainEventStorage<IDomainEvent>>()
                .Use<DomainEventStorage<IDomainEvent>>()
                .Ctor<string>("sqLiteConnectionString").Is(sqLiteConnectionString);

            For<IIdentityMap<IDomainEvent>>()
                .Use<EventStoreIdentityMap<IDomainEvent>>();

            For<IEventStoreUnitOfWork<IDomainEvent>>()
                .LifecycleIs(new UniquePerRequestLifecycle())
                .Use<EventStoreUnitOfWork<IDomainEvent>>();

            For<IUnitOfWork>()
                .Use(x => x.GetInstance<IEventStoreUnitOfWork<IDomainEvent>>());

            For<IDomainRepository<IDomainEvent>>()
                .Use<DomainRepository<IDomainEvent>>();
        }
    }
}