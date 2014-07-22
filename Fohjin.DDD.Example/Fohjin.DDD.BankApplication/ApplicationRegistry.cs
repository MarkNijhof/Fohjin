using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using StructureMap.Configuration.DSL;

namespace Fohjin.DDD.BankApplication
{
    public class ApplicationRegistry : Registry
    {
        public ApplicationRegistry()
        {
            For<IClientSearchFormPresenter>().UseIfNone<ClientSearchFormPresenter>();
            For<IClientDetailsPresenter>().UseIfNone<ClientDetailsPresenter>();
            For<IAccountDetailsPresenter>().UseIfNone<AccountDetailsPresenter>();
            For<IPopupPresenter>().UseIfNone<PopupPresenter>();

            For<IClientSearchFormView>().UseIfNone<ClientSearchForm>();
            For<IClientDetailsView>().UseIfNone<ClientDetails>();
            For<IAccountDetailsView>().UseIfNone<AccountDetails>();
            For<IPopupView>().UseIfNone<Popup>();
        }
    }
}