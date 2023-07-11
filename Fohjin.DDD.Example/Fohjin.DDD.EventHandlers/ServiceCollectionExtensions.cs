using Microsoft.Extensions.DependencyInjection;

namespace Fohjin.DDD.EventHandlers
{
    public static class ServiceCollectionExtensions
    {
        public static T AddEventHandlersServices<T>(this T service) where T : IServiceCollection
        {
            service.AddTransient<IEventHandler, AccountClosedEventHandler>();
            service.AddTransient<IEventHandler, AccountNameChangedEventHandler>();
            service.AddTransient<IEventHandler, AccountOpenedEventHandler>();
            service.AddTransient<IEventHandler, AccountToClientAssignedEventHandler>();
            service.AddTransient<IEventHandler, BankCardWasCanceledByClientEventHandler>();
            service.AddTransient<IEventHandler, BankCardWasReportedStolenEventHandler>();
            service.AddTransient<IEventHandler, CashDepositEventHandler>();
            service.AddTransient<IEventHandler, CashWithdrawnEventHandler>();
            service.AddTransient<IEventHandler, ClientCreatedEventHandler>();
            service.AddTransient<IEventHandler, ClientMovedEventHandler>();
            service.AddTransient<IEventHandler, ClientNameChangedEventHandler>();
            service.AddTransient<IEventHandler, ClientPhoneNumberChangedEventHandler>();
            service.AddTransient<IEventHandler, ClosedAccountCreatedEventHandler>();
            service.AddTransient<IEventHandler, MoneyTransferFailedEventHandler>();
            service.AddTransient<IEventHandler, MoneyTransferReceivedEventHandler>();
            service.AddTransient<IEventHandler, MoneyTransferSendEventHandler>();
            service.AddTransient<IEventHandler, NewBankCardForAccountAssignedEventHandler>();
            service.AddTransient<IEventHandler, SendMoneyTransferFurtherEventHandler>();

            return service;
        }
    }
}