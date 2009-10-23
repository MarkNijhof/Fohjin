using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using StructureMap.Configuration.DSL;

namespace Fohjin.DDD.BankApplication
{
    public class ApplicationRegistry : Registry
    {
        public ApplicationRegistry()
        {
            ForRequestedType<IClientSearchFormPresenter>().TheDefaultIsConcreteType<ClientSearchFormPresenter>();
            ForRequestedType<IClientDetailsPresenter>().TheDefaultIsConcreteType<ClientDetailsPresenter>();
            ForRequestedType<IAccountDetailsPresenter>().TheDefaultIsConcreteType<AccountDetailsPresenter>();
            ForRequestedType<IPopupPresenter>().TheDefaultIsConcreteType<PopupPresenter>();

            ForRequestedType<IClientSearchFormView>().TheDefaultIsConcreteType<ClientSearchForm>();
            ForRequestedType<IClientDetailsView>().TheDefaultIsConcreteType<ClientDetails>();
            ForRequestedType<IAccountDetailsView>().TheDefaultIsConcreteType<AccountDetails>();
            ForRequestedType<IPopupView>().TheDefaultIsConcreteType<Popup>();
        }
    }
}