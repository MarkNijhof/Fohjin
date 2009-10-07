using System.Data.SQLite;
using Fohjin.DDD.Domain.Entities;
using Fohjin.EventStorage;
using StructureMap.Configuration.DSL;

namespace Fohjin.DDD.Configuration
{
    public class DomainConfiguration : Registry
    {
        private const string dataBaseFile = "domainDataBase.db3";

        public DomainConfiguration()
        {
            ForRequestedType<SQLiteConnection>().TheDefault.Is.ConstructedBy(x => new SQLiteConnection(string.Format("Data Source={0}", dataBaseFile)));

            ForRequestedType<IDomainEventStorage>().TheDefault.Is.OfConcreteType<Storage>();
            ForRequestedType<ISnapShotStorage>().TheDefault.Is.OfConcreteType<Storage>();

            ForRequestedType<IRepository<ActiveAccount>>().TheDefault.Is.OfConcreteType<Repository<ActiveAccount>>();
        }
    }
}