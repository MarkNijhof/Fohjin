using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;

namespace Test.Fohjin.DDD.Scenarios.Adding_a_new_client
{
    public class When_in_the_GUI_the_new_client_form_is_displayed_ : PresenterTestFixture<ClientDetailsPresenter>
    {
        protected override void When()
        {
            Presenter.SetClient(null);
            Presenter.Display();
        }

        [Then]
        public void Then_the_save_button_will_be_disabled()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.DisableSaveButton()).WasCalled();
        }

        [Then]
        public void Then_the_menu_buttons_will_be_disabled()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.DisableAddNewAccountMenu()).WasCalled();
            On<IClientDetailsView>().VerifyThat.Method(x => x.DisableClientHasMovedMenu()).WasCalled();
            On<IClientDetailsView>().VerifyThat.Method(x => x.DisableNameChangedMenu()).WasCalled();
            On<IClientDetailsView>().VerifyThat.Method(x => x.DisablePhoneNumberChangedMenu()).WasCalled();
        }

        [Then]
        public void Then_overview_panel_will_be_shown()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.EnableOverviewPanel()).WasCalled();
        }

        [Then]
        public void Then_the_view_input_fields_will_be_reset()
        {
            On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.ClientName = string.Empty);
            On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.Street = string.Empty);
            On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.StreetNumber = string.Empty);
            On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.PostalCode = string.Empty);
            On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.City = string.Empty);
            On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.PhoneNumber = string.Empty);
            On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.Accounts = null);
            On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.ClosedAccounts = null);

            On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.ClosedAccounts = null);
        }

        [Then]
        public void Then_client_name_entry_panel_will_be_shown()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.EnableClientNamePanel()).WasCalled();
        }

        [Then]
        public void Then_show_dialog_will_be_called_on_the_view()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.ShowDialog()).WasCalled();
        }
    }
}