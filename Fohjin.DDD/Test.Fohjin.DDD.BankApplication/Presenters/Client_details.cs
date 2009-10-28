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
    public class When_displaying_the_client_details_view_for_an_existing_client : PresenterTestFixture<ClientDetailsPresenter>
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

    public class When_displaying_the_client_details_view_for_creating_a_new_client : PresenterTestFixture<ClientDetailsPresenter>
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

    public class When_saving_the_client_name_while_creating_a_new_client : PresenterTestFixture<ClientDetailsPresenter>
    {
        protected override void SetupDependencies()
        {
            OnDependency<IPopupPresenter>()
                .Setup(x => x.CatchPossibleException(It.IsAny<Action>()))
                .Callback<Action>(x => x());
        }

        protected override void Given()
        {
            Presenter.SetClient(null);
            Presenter.Display();
            On<IClientDetailsView>().ValueFor(x => x.ClientName).IsSetTo("New Client Name");
            On<IClientDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
        }

        protected override void When()
        {
            On<IClientDetailsView>().FireEvent(x => x.OnSaveNewClientName += null);
        }

        [Then]
        public void Then_the_save_button_will_be_disabled()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.DisableSaveButton()).WasCalled();
        }

        [Then]
        public void Then_overview_panel_will_be_shown()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.EnableAddressPanel()).WasCalled();
        }
    }

    public class When_saving_the_client_address_while_creating_a_new_client : PresenterTestFixture<ClientDetailsPresenter>
    {
        protected override void SetupDependencies()
        {
            OnDependency<IPopupPresenter>()
                .Setup(x => x.CatchPossibleException(It.IsAny<Action>()))
                .Callback<Action>(x => x());
        }

        protected override void Given()
        {
            Presenter.SetClient(null);
            Presenter.Display();
            On<IClientDetailsView>().ValueFor(x => x.ClientName).IsSetTo("New Client Name");
            On<IClientDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
            On<IClientDetailsView>().FireEvent(x => x.OnSaveNewClientName += null);

            On<IClientDetailsView>().ValueFor(x => x.Street).IsSetTo("Street");
            On<IClientDetailsView>().ValueFor(x => x.StreetNumber).IsSetTo("123");
            On<IClientDetailsView>().ValueFor(x => x.PostalCode).IsSetTo("5000");
            On<IClientDetailsView>().ValueFor(x => x.City).IsSetTo("Bergen");
            On<IClientDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
        }

        protected override void When()
        {
            On<IClientDetailsView>().FireEvent(x => x.OnSaveNewAddress += null);
        }

        [Then]
        public void Then_the_save_button_will_be_disabled()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.DisableSaveButton()).WasCalled();
        }

        [Then]
        public void Then_overview_panel_will_be_shown()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.EnablePhoneNumberPanel()).WasCalled();
        }
    }

    public class When_saving_the_client_phone_number_while_creating_a_new_client : PresenterTestFixture<ClientDetailsPresenter>
    {
        private CreateClientCommand CreateClientCommand;

        protected override void SetupDependencies()
        {
            OnDependency<IPopupPresenter>()
                .Setup(x => x.CatchPossibleException(It.IsAny<Action>()))
                .Callback<Action>(x => x());

            OnDependency<ICommandBus>()
                .Setup(x => x.Publish(It.IsAny<CreateClientCommand>()))
                .Callback<CreateClientCommand>(x => CreateClientCommand = x);
        }

        protected override void Given()
        {
            Presenter.SetClient(null);
            Presenter.Display();
            On<IClientDetailsView>().ValueFor(x => x.ClientName).IsSetTo("New Client Name");
            On<IClientDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
            On<IClientDetailsView>().FireEvent(x => x.OnSaveNewClientName += null);

            On<IClientDetailsView>().ValueFor(x => x.Street).IsSetTo("Street");
            On<IClientDetailsView>().ValueFor(x => x.StreetNumber).IsSetTo("123");
            On<IClientDetailsView>().ValueFor(x => x.PostalCode).IsSetTo("5000");
            On<IClientDetailsView>().ValueFor(x => x.City).IsSetTo("Bergen");
            On<IClientDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
            On<IClientDetailsView>().FireEvent(x => x.OnSaveNewAddress += null);

            On<IClientDetailsView>().ValueFor(x => x.PhoneNumber).IsSetTo("1234567890");
            On<IClientDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
        }

        protected override void When()
        {
            On<IClientDetailsView>().FireEvent(x => x.OnSaveNewPhoneNumber += null);
        }

        [Then]
        public void Then_the_save_button_will_be_disabled()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.DisableSaveButton()).WasCalled();
        }

        [Then]
        public void Then_a_create_client_command_with_all_collected_information_will_be_published()
        {
            On<ICommandBus>().VerifyThat.Method(x => x.Publish(It.IsAny<CreateClientCommand>())).WasCalled();

            CreateClientCommand.ClientName.WillBe("New Client Name");
            CreateClientCommand.Street.WillBe("Street");
            CreateClientCommand.StreetNumber.WillBe("123");
            CreateClientCommand.PostalCode.WillBe("5000");
            CreateClientCommand.City.WillBe("Bergen");
            CreateClientCommand.PhoneNumber.WillBe("1234567890");
        }

        [Then]
        public void Then_overview_panel_will_be_shown()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.Close()).WasCalled();
        }
    }
}