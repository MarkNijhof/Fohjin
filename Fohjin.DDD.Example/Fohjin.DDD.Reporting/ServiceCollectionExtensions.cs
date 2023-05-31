using Fohjin.DDD.Reporting.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Fohjin.DDD.Reporting
{
    public static class ServiceCollectionExtensions
    {
        private const string sqLiteConnectionString = "Data Source=reportingDataBase.db3";

        public static T AddReportingServices<T>(this T service) where T : IServiceCollection
        {
            service.TryAddTransient<ISqlCreateBuilder, SqlCreateBuilder>();
            service.TryAddTransient<ISqlInsertBuilder, SqlInsertBuilder>();
            service.TryAddTransient<ISqlSelectBuilder, SqlSelectBuilder>();
            service.TryAddTransient<ISqlUpdateBuilder, SqlUpdateBuilder>();
            service.TryAddTransient<ISqlDeleteBuilder, SqlDeleteBuilder>();

            service.TryAddTransient<IReportingRepository>(sp => ActivatorUtilities.CreateInstance<SqliteReportingRepository>(sp, sqLiteConnectionString));

            return service;
        }
    }
}