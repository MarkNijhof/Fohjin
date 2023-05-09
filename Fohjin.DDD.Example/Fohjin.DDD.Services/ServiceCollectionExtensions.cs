using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Fohjin.DDD.Services
{
    public static class ServiceCollectionExtensions
    {
        public static T AddDddServices<T> (this T service) where T : IServiceCollection
        {
            service.TryAddTransient<IReceiveMoneyTransfers, MoneyReceiveService>();
            service.TryAddTransient<ISendMoneyTransfer, MoneyTransferService>();
            return service;
        }
    }
}