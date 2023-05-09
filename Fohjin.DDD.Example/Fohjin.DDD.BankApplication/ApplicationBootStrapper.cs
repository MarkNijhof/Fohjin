using Microsoft.Extensions.DependencyInjection;

namespace Fohjin.DDD.BankApplication
{
    public static class ServiceProviderExtensions
    {
        public static T BootStrapApplication<T>(this T serviceProvider) where T : IServiceProvider
        {
            ActivatorUtilities.CreateInstance<DomainDatabaseBootStrapper>(serviceProvider).CreateDatabaseSchemaIfNeeded();
            ActivatorUtilities.CreateInstance<ReportingDatabaseBootStrapper>(serviceProvider).CreateDatabaseSchemaIfNeeded();

            //ActivatorUtilities.CreateInstance<RegisterCommandHandlersInMessageRouter>(serviceProvider);
            //ActivatorUtilities.CreateInstance<RegisterEventHandlersInMessageRouter>(serviceProvider);

            return serviceProvider;
        }
    }
}