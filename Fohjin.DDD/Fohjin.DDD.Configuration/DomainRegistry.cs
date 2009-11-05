using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Fohjin.DDD.Bus.Implementation;
using Fohjin.DDD.EventStore;
using Fohjin.DDD.EventStore.Bus;
using Fohjin.DDD.EventStore.SQLite;
using Fohjin.DDD.EventStore.Storage;
using StructureMap.Configuration.DSL;

namespace Fohjin.DDD.Configuration
{
    public class DomainRegistry : Registry
    {
        private const string sqLiteConnectionString = "Data Source=domainDataBase.db3";

        public DomainRegistry()
        {
            ForRequestedType<IQueue>().TheDefault.Is.OfConcreteType<InMemoryQueue>();
            ForRequestedType<IEventBus>().AsSingletons().TheDefault.Is.OfConcreteType<DirectEventBus>();

            ForRequestedType<ICommandBus>().TheDefault.Is.OfConcreteType<DirectCommandBus>();
            ForRequestedType<IFormatter>().TheDefault.Is.ConstructedBy(x => new BinaryFormatter());
            ForRequestedType<IDomainEventStorage>().TheDefault.Is.OfConcreteType<DomainEventStorage>().WithCtorArg("sqLiteConnectionString").EqualTo(sqLiteConnectionString);

            ForRequestedType<IIdentityMap>().TheDefault.Is.OfConcreteType<EventStoreIdentityMap>();
            ForRequestedType<IEventStoreUnitOfWork>().TheDefault.Is.OfConcreteType<EventStoreUnitOfWork>();
            ForRequestedType<IEventHandlerUnitOfWork>().TheDefault.Is.OfConcreteType<EventHandlerUnitOfWork>();
            ForRequestedType<IDomainRepository>().TheDefault.Is.OfConcreteType<DomainRepository>();
        }
    }
}