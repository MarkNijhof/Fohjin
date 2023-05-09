using Microsoft.Extensions.DependencyInjection;

namespace Fohjin.DDD.Commands
{
    public static class ServiceCollectionExtensions
    {
        public static T AddCommandsServices<T>(this T service) where T : IServiceCollection
        {
            service.AddTransient<ICommand, AssignNewBankCardCommand>();
            service.AddTransient<ICommand, CancelBankCardCommand>();
            service.AddTransient<ICommand, ChangeAccountNameCommand>();
            service.AddTransient<ICommand, ChangeClientNameCommand>();
            service.AddTransient<ICommand, ChangeClientPhoneNumberCommand>();
            service.AddTransient<ICommand, ClientIsMovingCommand>();
            service.AddTransient<ICommand, CloseAccountCommand>();
            service.AddTransient<ICommand, CreateClientCommand>();
            service.AddTransient<ICommand, DepositCashCommand>();
            service.AddTransient<ICommand, MoneyTransferFailedCompensatingCommand>();
            service.AddTransient<ICommand, OpenNewAccountForClientCommand>();
            service.AddTransient<ICommand, ReceiveMoneyTransferCommand>();
            service.AddTransient<ICommand, ReportStolenBankCardCommand>();
            service.AddTransient<ICommand, SendMoneyTransferCommand>();
            service.AddTransient<ICommand, WithdrawalCashCommand>();
            return service;
        }
    }
}