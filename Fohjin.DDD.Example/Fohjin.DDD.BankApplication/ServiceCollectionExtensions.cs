using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Fohjin.DDD.BankApplication;

public static class ServiceCollectionExtensions
{
    public static T AddBankApplicationServices<T>(this T service) where T : IServiceCollection
    {
        service.TryAddTransient<IClientSearchFormPresenter, ClientSearchFormPresenter>();
        service.TryAddTransient<IClientDetailsPresenter, ClientDetailsPresenter>();
        service.TryAddTransient<IAccountDetailsPresenter, AccountDetailsPresenter>();
        service.TryAddTransient<IPopupPresenter, PopupPresenter>();

        service.TryAddTransient<IClientSearchFormView, ClientSearchForm>();
        service.TryAddTransient<IClientDetailsView, ClientDetails>();
        service.TryAddTransient<IAccountDetailsView, AccountDetails>();
        service.TryAddTransient<IPopupView, Popup>();

        return service;
    }
}