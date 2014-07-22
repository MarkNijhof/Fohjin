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
            For<ISqlCreateBuilder>().Use<SqlCreateBuilder>();
            For<ISqlInsertBuilder>().Use<SqlInsertBuilder>();
            For<ISqlSelectBuilder>().Use<SqlSelectBuilder>();
            For<ISqlUpdateBuilder>().Use<SqlUpdateBuilder>();
            For<ISqlDeleteBuilder>().Use<SqlDeleteBuilder>();

            For<IReportingRepository>().Use<SQLiteReportingRepository>().Ctor<string>("sqLiteConnectionString").Is(sqLiteConnectionString);
        }
    }
}