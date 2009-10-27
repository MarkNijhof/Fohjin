using System;
using System.Collections.Generic;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
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

    public class When_displaying_the_client_details_view_for_creating_a_new_client : BaseTestFixture<ClientDetailsPresenter>
    {
        protected override void When()
        {
            SubjectUnderTest.SetClient(null);
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
        public void Then_the_view_input_fields_will_be_reset()
        {
            GetMock<IClientDetailsView>().VerifySet(x => x.ClientName = string.Empty);
            GetMock<IClientDetailsView>().VerifySet(x => x.Street = string.Empty);
            GetMock<IClientDetailsView>().VerifySet(x => x.StreetNumber = string.Empty);
            GetMock<IClientDetailsView>().VerifySet(x => x.PostalCode = string.Empty);
            GetMock<IClientDetailsView>().VerifySet(x => x.City = string.Empty);
            GetMock<IClientDetailsView>().VerifySet(x => x.PhoneNumber = string.Empty);
            GetMock<IClientDetailsView>().VerifySet(x => x.Accounts = null);
            GetMock<IClientDetailsView>().VerifySet(x => x.ClosedAccounts = null);
        }

        [Then]
        public void Then_client_name_entry_panel_will_be_shown()
        {
            GetMock<IClientDetailsView>().Verify(x => x.EnableClientNamePanel());
        }

        [Then]
        public void Then_show_dialog_will_be_called_on_the_view()
        {
            GetMock<IClientDetailsView>().Verify(x => x.ShowDialog());
        }
    }

    public class When_saving_the_client_name_while_creating_a_new_client : BaseTestFixture<ClientDetailsPresenter>
    {
        protected override void MockSetup()
        {
            GetMock<IPopupPresenter>()
                .Setup(x => x.CatchPossibleException(It.IsAny<System.Action>()))
                .Callback<System.Action>(x => x());
        }

        protected override void Given()
        {
            SubjectUnderTest.SetClient(null);
            SubjectUnderTest.Display();
            GetMock<IClientDetailsView>().SetupGet(x => x.ClientName).Returns("New Client Name");
            GetMock<IClientDetailsView>().Raise(x => x.OnFormElementGotChanged += delegate { });
        }

        protected override void When()
        {
            GetMock<IClientDetailsView>().Raise(x => x.OnSaveNewClientName += delegate { });
        }

        [Then]
        public void Then_the_save_button_will_be_disabled()
        {
            GetMock<IClientDetailsView>().Verify(x => x.DisableSaveButton());
        }

        [Then]
        public void Then_overview_panel_will_be_shown()
        {
            GetMock<IClientDetailsView>().Verify(x => x.EnableAddressPanel());
        }
    }

    public class When_saving_the_client_address_while_creating_a_new_client : BaseTestFixture<ClientDetailsPresenter>
    {
        protected override void MockSetup()
        {
            GetMock<IPopupPresenter>()
                .Setup(x => x.CatchPossibleException(It.IsAny<System.Action>()))
                .Callback<System.Action>(x => x());
        }

        protected override void Given()
        {
            SubjectUnderTest.SetClient(null);
            SubjectUnderTest.Display();
            GetMock<IClientDetailsView>().SetupGet(x => x.ClientName).Returns("New Client Name");
            GetMock<IClientDetailsView>().Raise(x => x.OnFormElementGotChanged += delegate { });
            GetMock<IClientDetailsView>().Raise(x => x.OnSaveNewClientName += delegate { });
            GetMock<IClientDetailsView>().SetupGet(x => x.Street).Returns("Street");
            GetMock<IClientDetailsView>().SetupGet(x => x.StreetNumber).Returns("123");
            GetMock<IClientDetailsView>().SetupGet(x => x.PostalCode).Returns("5000");
            GetMock<IClientDetailsView>().SetupGet(x => x.City).Returns("Bergen");
            GetMock<IClientDetailsView>().Raise(x => x.OnFormElementGotChanged += delegate { });
        }

        protected override void When()
        {
            GetMock<IClientDetailsView>().Raise(x => x.OnSaveNewAddress += delegate { });
        }

        [Then]
        public void Then_the_save_button_will_be_disabled()
        {
            GetMock<IClientDetailsView>().Verify(x => x.DisableSaveButton());
        }

        [Then]
        public void Then_overview_panel_will_be_shown()
        {
            GetMock<IClientDetailsView>().Verify(x => x.EnablePhoneNumberPanel());
        }
    }

    public class When_saving_the_client_phone_number_while_creating_a_new_client : BaseTestFixture<ClientDetailsPresenter>
    {
        private CreateClientCommand _createClientCommand;

        protected override void MockSetup()
        {
            GetMock<IPopupPresenter>()
                .Setup(x => x.CatchPossibleException(It.IsAny<System.Action>()))
                .Callback<System.Action>(x => x());

            GetMock<ICommandBus>()
                .Setup(x => x.Publish(It.IsAny<CreateClientCommand>()))
                .Callback<CreateClientCommand>(x => _createClientCommand = x);
        }

        protected override void Given()
        {
            SubjectUnderTest.SetClient(null);
            SubjectUnderTest.Display();
            GetMock<IClientDetailsView>().SetupGet(x => x.ClientName).Returns("New Client Name");
            GetMock<IClientDetailsView>().Raise(x => x.OnFormElementGotChanged += delegate { });
            GetMock<IClientDetailsView>().Raise(x => x.OnSaveNewClientName += delegate { });
            GetMock<IClientDetailsView>().SetupGet(x => x.Street).Returns("Street");
            GetMock<IClientDetailsView>().SetupGet(x => x.StreetNumber).Returns("123");
            GetMock<IClientDetailsView>().SetupGet(x => x.PostalCode).Returns("5000");
            GetMock<IClientDetailsView>().SetupGet(x => x.City).Returns("Bergen");
            GetMock<IClientDetailsView>().Raise(x => x.OnFormElementGotChanged += delegate { });
            GetMock<IClientDetailsView>().Raise(x => x.OnSaveNewAddress += delegate { });
            GetMock<IClientDetailsView>().SetupGet(x => x.PhoneNumber).Returns("1234567890");
            GetMock<IClientDetailsView>().Raise(x => x.OnFormElementGotChanged += delegate { });
        }

        protected override void When()
        {
            GetMock<IClientDetailsView>().Raise(x => x.OnSaveNewPhoneNumber += delegate { });
        }

        [Then]
        public void Then_the_save_button_will_be_disabled()
        {
            GetMock<IClientDetailsView>().Verify(x => x.DisableSaveButton());
        }

        [Then]
        public void Then_a_create_client_command_with_all_collected_information_will_be_published()
        {
            GetMock<ICommandBus>().Verify(x => x.Publish(It.IsAny<CreateClientCommand>()));
            _createClientCommand.ClientName.WillBe("New Client Name");
            _createClientCommand.Street.WillBe("Street");
            _createClientCommand.StreetNumber.WillBe("123");
            _createClientCommand.PostalCode.WillBe("5000");
            _createClientCommand.City.WillBe("Bergen");
            _createClientCommand.PhoneNumber.WillBe("1234567890");
        }

        [Then]
        public void Then_overview_panel_will_be_shown()
        {
            GetMock<IClientDetailsView>().Verify(x => x.Close());
        }
    }
}