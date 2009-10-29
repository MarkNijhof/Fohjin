using System;
using System.Collections.Generic;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Reporting.Dto;
using Moq;

namespace Test.Fohjin.DDD.Scenarios
{
    public class When_opening_an_existing_client : PresenterTestFixture<ClientSearchFormPresenter>
    {
        private ClientReport _clientReport;

        protected override void SetupDependencies()
        {
            OnDependency<IPopupPresenter>()
                .Setup(x => x.CatchPossibleException(It.IsAny<Action>()))
                .Callback<Action>(x => x());

            _clientReport = new ClientReport(Guid.NewGuid(), "Client Name");

            OnDependency<IClientSearchFormView>()
                .Setup(x => x.GetSelectedClient())
                .Returns(_clientReport);
        }

        protected override void When()
        {
            On<IClientSearchFormView>().FireEvent(x => x.OnOpenSelectedClient += delegate { });
        }

        [Then]
        public void Then_get_selected_client_will_be_called_on_the_view()
        {
            On<IClientSearchFormView>().VerifyThat.Method(x => x.GetSelectedClient()).WasCalled();
        }

        [Then]
        public void Then_client_report_data_from_the_reporting_repository_is_being_loaded_into_the_view()
        {
            On<IClientDetailsPresenter>().VerifyThat.Method(x => x.SetClient(_clientReport)).WasCalled();
        }

        [Then]
        public void Then_display_will_be_called_on_the_view()
        {
            On<IClientDetailsPresenter>().VerifyThat.Method(x => x.Display()).WasCalled();
        }
    }

    public class When_displaying_client_details : PresenterTestFixture<ClientDetailsPresenter>
    {
        private readonly Guid _clientId = Guid.NewGuid();
        private ClientDetailsReport _clientDetailsReport;
        private List<ClientDetailsReport> _clientDetailsReports;

        protected override void SetupDependencies()
        {
            _clientDetailsReport = new ClientDetailsReport(_clientId, "Client Name", "street", "123", "5000", "bergen", "1234567890");
            _clientDetailsReports = new List<ClientDetailsReport> { _clientDetailsReport };
            OnDependency<IReportingRepository>()
                .Setup(x => x.GetByExample<ClientDetailsReport>(It.IsAny<object>()))
                .Returns(_clientDetailsReports);
        }

        protected override void When()
        {
            Presenter.SetClient(new ClientReport(_clientId, "Client Name"));
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
        public void Then_client_details_report_data_from_the_reporting_repository_is_being_loaded_into_the_view()
        {
            On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.ClientName = _clientDetailsReport.ClientName);
            On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.Street = _clientDetailsReport.Street);
            On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.StreetNumber = _clientDetailsReport.StreetNumber);
            On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.PostalCode = _clientDetailsReport.PostalCode);
            On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.City = _clientDetailsReport.City);
            On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.PhoneNumber = _clientDetailsReport.PhoneNumber);
            On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.Accounts = _clientDetailsReport.Accounts);
            On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.ClosedAccounts = _clientDetailsReport.ClosedAccounts);

            On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.ClientNameLabel = _clientDetailsReport.ClientName);
            On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.PhoneNumberLabel = _clientDetailsReport.PhoneNumber);
            On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.AddressLine1Label = string.Format("{0} {1}", _clientDetailsReport.Street, _clientDetailsReport.StreetNumber));
            On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.AddressLine2Label = string.Format("{0} {1}", _clientDetailsReport.PostalCode, _clientDetailsReport.City));
        }

        [Then]
        public void Then_the_menu_buttons_will_be_enabled()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.EnableAddNewAccountMenu()).WasCalled();
            On<IClientDetailsView>().VerifyThat.Method(x => x.EnableClientHasMovedMenu()).WasCalled();
            On<IClientDetailsView>().VerifyThat.Method(x => x.EnableNameChangedMenu()).WasCalled();
            On<IClientDetailsView>().VerifyThat.Method(x => x.EnablePhoneNumberChangedMenu()).WasCalled();
        }

        [Then]
        public void Then_show_dialog_will_be_called_on_the_view()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.ShowDialog()).WasCalled();
        }
    }
}