using System;
using System.Collections.Generic;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Reporting.Dto;
using Moq;

namespace Test.Fohjin.DDD.BankApplication.Presenters
{
    public class When_displaying_the_client_details_view_for_an_existing_client : BaseTestFixture<ClientDetailsPresenter>
    {
        private readonly Guid _clientId = Guid.NewGuid();
        private ClientDetailsReport _clientDetailsReport;
        private List<ClientDetailsReport> _clientDetailsReports;

        protected override void MockSetup()
        {
            _clientDetailsReport = new ClientDetailsReport(_clientId, "Client Name", "street", "123", "5000", "bergen", "1234567890");
            _clientDetailsReports = new List<ClientDetailsReport> { _clientDetailsReport };
            GetMock<IReportingRepository>()
                .Setup(x => x.GetByExample<ClientDetailsReport>(It.IsAny<object>()))
                .Returns(_clientDetailsReports);
        }

        protected override void Given()
        {
        }

        protected override void When()
        {
            SubjectUnderTest.SetClient(new ClientReport(_clientId, "Client Name"));
            SubjectUnderTest.Display();
        }

        [Then]
        public void Then_the_save_button_will_be_disabled()
        {
            GetMock<IClientDetailsView>().Verify(x => x.DisableSaveButton());
        }

        [Then]
        public void Then_the_menu_buttons_will_be_disabled()
        {
            GetMock<IClientDetailsView>().Verify(x => x.DisableAddNewAccountMenu());
            GetMock<IClientDetailsView>().Verify(x => x.DisableClientHasMovedMenu());
            GetMock<IClientDetailsView>().Verify(x => x.DisableNameChangedMenu());
            GetMock<IClientDetailsView>().Verify(x => x.DisablePhoneNumberChangedMenu());
        }

        [Then]
        public void Then_overview_panel_will_be_shown()
        {
            GetMock<IClientDetailsView>().Verify(x => x.EnableOverviewPanel());
        }

        [Then]
        public void Then_client_details_report_data_from_the_reporting_repository_is_being_loaded_into_the_view()
        {
            GetMock<IClientDetailsView>().VerifySet(x => x.ClientName = _clientDetailsReport.ClientName);
            GetMock<IClientDetailsView>().VerifySet(x => x.Street = _clientDetailsReport.Street);
            GetMock<IClientDetailsView>().VerifySet(x => x.StreetNumber = _clientDetailsReport.StreetNumber);
            GetMock<IClientDetailsView>().VerifySet(x => x.PostalCode = _clientDetailsReport.PostalCode);
            GetMock<IClientDetailsView>().VerifySet(x => x.City = _clientDetailsReport.City);
            GetMock<IClientDetailsView>().VerifySet(x => x.PhoneNumber = _clientDetailsReport.PhoneNumber);
            GetMock<IClientDetailsView>().VerifySet(x => x.Accounts = _clientDetailsReport.Accounts);
            GetMock<IClientDetailsView>().VerifySet(x => x.ClosedAccounts = _clientDetailsReport.ClosedAccounts);

            GetMock<IClientDetailsView>().VerifySet(x => x.ClientNameLabel = _clientDetailsReport.ClientName);
            GetMock<IClientDetailsView>().VerifySet(x => x.PhoneNumberLabel = _clientDetailsReport.PhoneNumber);
            GetMock<IClientDetailsView>().VerifySet(x => x.AddressLine1Label = string.Format("{0} {1}", _clientDetailsReport.Street, _clientDetailsReport.StreetNumber));
            GetMock<IClientDetailsView>().VerifySet(x => x.AddressLine2Label = string.Format("{0} {1}", _clientDetailsReport.PostalCode, _clientDetailsReport.City));
        }

        [Then]
        public void Then_the_menu_buttons_will_be_enabled()
        {
            GetMock<IClientDetailsView>().Verify(x => x.EnableAddNewAccountMenu());
            GetMock<IClientDetailsView>().Verify(x => x.EnableClientHasMovedMenu());
            GetMock<IClientDetailsView>().Verify(x => x.EnableNameChangedMenu());
            GetMock<IClientDetailsView>().Verify(x => x.EnablePhoneNumberChangedMenu());
        }

        [Then]
        public void Then_show_dialog_will_be_called_on_the_view()
        {
            GetMock<IClientDetailsView>().Verify(x => x.ShowDialog());
        }
    }
}