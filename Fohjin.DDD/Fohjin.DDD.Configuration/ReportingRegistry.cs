using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Infrastructure;
using StructureMap.Configuration.DSL;

namespace Fohjin.DDD.Configuration
{
    public class ReportingRegistry : Registry
    {
        private const string sqLiteConnectionString = "Data Source=reportingDataBase.db3";

        public ReportingRegistry()
        {
            ForRequestedType<ISqlCreateBuilder>().TheDefault.Is.OfConcreteType<SqlCreateBuilder>();
            ForRequestedType<ISqlInsertBuilder>().TheDefault.Is.OfConcreteType<SqlInsertBuilder>();
            ForRequestedType<ISqlSelectBuilder>().TheDefault.Is.OfConcreteType<SqlSelectBuilder>();
            ForRequestedType<ISqlUpdateBuilder>().TheDefault.Is.OfConcreteType<SqlUpdateBuilder>();
            ForRequestedType<ISqlDeleteBuilder>().TheDefault.Is.OfConcreteType<SqlDeleteBuilder>();

            ForRequestedType<IReportingRepository>().TheDefault.Is.OfConcreteType<SQLiteReportingRepository>()
                .WithCtorArg("sqLiteConnectionString").EqualTo(sqLiteConnectionString);
        }
    }
}