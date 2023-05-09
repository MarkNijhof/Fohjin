using Fohjin.DDD.Bus.Direct;
using Fohjin.DDD.Reporting.Infrastructure;
using Fohjin.DDD.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Fohjin.DDD.Reporting
{
    public static class ServiceCollectionExtensions
    {
        private const string sqLiteConnectionString = "Data Source=reportingDataBase.db3";

        public static T AddConfigurationServices<T>(this T service) where T : IServiceCollection
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