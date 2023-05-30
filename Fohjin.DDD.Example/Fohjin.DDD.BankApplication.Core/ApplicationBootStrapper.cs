using Fohjin.DDD.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fohjin.DDD.BankApplication
{
    public static class ServiceProviderExtensions
    {
        public static T BootStrapApplication<T>(this T serviceProvider) where T : IServiceProvider
        {
            var dataBaseFile = Path.GetFullPath(DomainDatabaseBootStrapper.DataBaseFile);
            var reportingFile = Path.GetFullPath(ReportingDatabaseBootStrapper.ReportingDataBaseFile);

            ActivatorUtilities.CreateInstance<DomainDatabaseBootStrapper>(serviceProvider)
                .CreateDatabaseSchemaIfNeeded(dataBaseFile);
            ActivatorUtilities.CreateInstance<ReportingDatabaseBootStrapper>(serviceProvider)
                .CreateDatabaseSchemaIfNeeded(reportingFile);

            return serviceProvider;
        }
    }
}