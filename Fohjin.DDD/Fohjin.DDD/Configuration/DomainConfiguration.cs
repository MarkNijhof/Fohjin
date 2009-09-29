using System.Data.SQLite;
using Fohjin.DDD.Domain.Contracts;
using Fohjin.DDD.Domain.Repositories;
using Fohjin.DDD.Infrastructure;
using StructureMap.Configuration.DSL;

namespace Fohjin.DDD.Configuration
{
    public class DomainConfiguration : Registry
    {
        private const string dataBaseFile = "domainDataBase.db3";

        public DomainConfiguration()
        {
            ForRequestedType<SQLiteConnection>().TheDefault.Is.ConstructedBy(x => new SQLiteConnection(string.Format("Data Source={0}", dataBaseFile)));

            ForRequestedType<IDomainEventStorage>().TheDefault.Is.OfConcreteType<DomainEventStorage>();
            ForRequestedType<ISnapShotStorage>().TheDefault.Is.OfConcreteType<SnapShotStorage>();

            ForRequestedType<ActiveAccountRepository>().TheDefault.Is.OfConcreteType<ActiveAccountRepository>();
        }
    }
}