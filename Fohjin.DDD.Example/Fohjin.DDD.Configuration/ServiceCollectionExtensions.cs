using Fohjin.DDD.CommandHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Fohjin.DDD.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static T AddConfigurationServices<T>(this T service) where T : IServiceCollection
        {
            service.TryAddSingleton<ICommandHandlerHelper, CommandHandlerHelper>();
            return service;
        }
    }
}