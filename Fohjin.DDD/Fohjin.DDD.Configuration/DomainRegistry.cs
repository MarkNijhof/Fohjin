using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Bus.Implementation;
using Fohjin.DDD.Domain.Entities;
using Fohjin.EventStorage;
using StructureMap.Configuration.DSL;

namespace Fohjin.DDD.Configuration
{
    public class DomainRegistry : Registry
    {
        private const string sqLiteConnectionString = "Data Source=domainDataBase.db3";

        public DomainRegistry()
        {
            ForRequestedType<ICommandBus>().TheDefault.Is.OfConcreteType<DirectCommandBus>();
            ForRequestedType<IFormatter>().TheDefault.Is.ConstructedBy(x => new BinaryFormatter());
            ForRequestedType<IDomainEventStorage>().TheDefault.Is.OfConcreteType<Storage>().WithCtorArg("sqLiteConnectionString").EqualTo(sqLiteConnectionString);
            ForRequestedType<ISnapShotStorage>().TheDefault.Is.OfConcreteType<Storage>().WithCtorArg("sqLiteConnectionString").EqualTo(sqLiteConnectionString);

            ForRequestedType<IRepository<ActiveAccount>>().TheDefault.Is.OfConcreteType<Repository<ActiveAccount>>();
            ForRequestedType<IRepository<Client>>().TheDefault.Is.OfConcreteType<Repository<Client>>();
        }
    }
}