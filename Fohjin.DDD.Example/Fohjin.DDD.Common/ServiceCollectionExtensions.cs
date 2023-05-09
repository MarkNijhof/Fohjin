using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Fohjin.DDD.Common
{
    public static class ServiceCollectionExtensions
    {
        public static T AddCommonServices<T>(this T service) where T : IServiceCollection
        {
            service.TryAddTransient<IExtendedFormatter, ExtendedFormatter>();
            return service;
        }
    }
}