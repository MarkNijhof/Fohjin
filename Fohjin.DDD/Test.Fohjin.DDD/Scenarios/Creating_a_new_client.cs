using System;
using System.Linq;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Bus;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting.Dto;
using Moq;

namespace Test.Fohjin.DDD.Scenarios
{
    public class When_starting_to_create_a_new_client : PresenterTestFixture<ClientSearchFormPresenter>
    {
        protected override void When()
        {
            On<IClientSearchFormView>().FireEvent(x => x.OnCreateNewClient += delegate { });
        }

        [Then]
        public void Then_client_report_data_from_the_reporting_repository_is_being_loaded_into_the_view()
        {
            On<IClientDetailsPresenter>().VerifyThat.Method(x => x.SetClient(null)).WasCalled();
        }

        [Then]
        public void Then_display_will_be_called_on_the_view()
        {
            On<IClientDetailsPresenter>().VerifyThat.Method(x => x.Display()).WasCalled();
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

    public class When_creating_a_new_client : CommandTestFixture<CreateClientCommand, CreateClientCommandHandler, Client>
    {
        protected override CreateClientCommand When()
        {
            return new CreateClientCommand(Guid.NewGuid(), "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937");
        }

        [Then]
        public void Then_a_client_created_event_will_be_published()
        {
            PublishedEvents.Last().WillBeOfType<ClientCreatedEvent>();
        }

        [Then]
        public void Then_the_published_event_will_contain_the_name_of_the_client()
        {
            PublishedEvents.Last<ClientCreatedEvent>().ClientName.WillBe("Mark Nijhof");
        }

        [Then]
        public void Then_the_published_event_will_contain_the_address_of_the_client()
        {
            PublishedEvents.Last<ClientCreatedEvent>().Street.WillBe("Welhavens gate");
            PublishedEvents.Last<ClientCreatedEvent>().StreetNumber.WillBe("49b");
            PublishedEvents.Last<ClientCreatedEvent>().PostalCode.WillBe("5006");
            PublishedEvents.Last<ClientCreatedEvent>().City.WillBe("Bergen");
        }

        [Then]
        public void Then_the_published_event_will_contain_the_phone_number_of_the_client()
        {
            PublishedEvents.Last<ClientCreatedEvent>().PhoneNumber.WillBe("95009937");
        }
    }

    public class When_a_new_client_was_created : EventTestFixture<ClientCreatedEvent, ClientCreatedEventHandler>
    {
        private static Guid _clientId;
        private ClientReport SaveClientObject;
        private ClientDetailsReport SaveClientDetailsObject;

        protected override void SetupDependencies()
        {
            OnDependency<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<ClientReport>()))
                .Callback<ClientReport>(a => SaveClientObject = a);

            OnDependency<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<ClientDetailsReport>()))
                .Callback<ClientDetailsReport>(a => SaveClientDetailsObject = a);
        }

        protected override ClientCreatedEvent When()
        {
            _clientId = Guid.NewGuid();
            var clientCreatedEvent = new ClientCreatedEvent(_clientId, "New Client Name", "Street", "123", "5000", "Bergen", "1234567890");
            return clientCreatedEvent;
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_save_the_client_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Save(It.IsAny<ClientReport>()));
        }

        [Then]
        public void Then_the_client_report_will_be_updated_with_the_expected_details()
        {
            SaveClientObject.Id.WillBe(_clientId);
            SaveClientObject.Name.WillBe("New Client Name");
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_save_the_client_details_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Save(It.IsAny<ClientDetailsReport>()));
        }

        [Then]
        public void Then_the_client_details_report_will_be_updated_with_the_expected_details()
        {
            SaveClientDetailsObject.Id.WillBe(_clientId);
            SaveClientDetailsObject.ClientName.WillBe("New Client Name");
            SaveClientDetailsObject.Street.WillBe("Street");
            SaveClientDetailsObject.StreetNumber.WillBe("123");
            SaveClientDetailsObject.PostalCode.WillBe("5000");
            SaveClientDetailsObject.City.WillBe("Bergen");
            SaveClientDetailsObject.PhoneNumber.WillBe("1234567890");
        }
    }
}