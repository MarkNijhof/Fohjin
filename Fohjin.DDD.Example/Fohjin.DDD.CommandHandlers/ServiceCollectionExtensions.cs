using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Fohjin.DDD.CommandHandlers
{
    public static class ServiceCollectionExtensions
    {
        public static T AddCommandHandlersServices<T>(this T service) where T : IServiceCollection
        {
            service.AddTransient<ICommandHandler, AssignNewBankCardCommandHandler>();
            service.AddTransient<ICommandHandler, CancelBankCardCommandHandler>();
            service.AddTransient<ICommandHandler, ChangeAccountNameCommandHandler>();
            service.AddTransient<ICommandHandler, ChangeClientNameCommandHandler>();
            service.AddTransient<ICommandHandler, ChangeClientPhoneNumberCommandHandler>();
            service.AddTransient<ICommandHandler, ClientIsMovingCommandHandler>();
            service.AddTransient<ICommandHandler, CloseAccountCommandHandler>();
            service.AddTransient<ICommandHandler, CreateClientCommandHandler>();
            service.AddTransient<ICommandHandler, DepositCashCommandHandler>();
            service.AddTransient<ICommandHandler, MoneyTransferFailedCompensatingCommandHandler>();
            service.AddTransient<ICommandHandler, OpenNewAccountForClientCommandHandler>();
            service.AddTransient<ICommandHandler, ReceiveMoneyTransferCommandHandler>();
            service.AddTransient<ICommandHandler, ReportStolenBankCardCommandHandler>();
            service.AddTransient<ICommandHandler, SendMoneyTransferCommandHandler>();
            service.AddTransient<ICommandHandler, WithdrawalCashCommandHandler>();

            service.TryAddTransient(typeof(ITransactionHandler<,>), typeof(TransactionHandler<,>));

            return service;
        }
    }
}