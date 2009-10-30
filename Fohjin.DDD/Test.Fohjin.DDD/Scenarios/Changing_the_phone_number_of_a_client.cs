using System;
using System.Linq;
using System.Collections.Generic;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Bus;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.Exceptions;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting.Dto;
using Moq;

namespace Test.Fohjin.DDD.Scenarios
{
    public class When_clicking_to_change_a_client_phone_number : PresenterTestFixture<ClientDetailsPresenter>
    {
        private readonly Guid _clientId = Guid.NewGuid();
        private ClientDetailsReport _clientDetailsReport;
        private List<ClientDetailsReport> _clientDetailsReports;

        protected override void SetupDependencies()
        {
            _clientDetailsReport = new ClientDetailsReport(_clientId, "Client Name", "Street", "123", "5000", "Bergen", "1234567890");
            _clientDetailsReports = new List<ClientDetailsReport> { _clientDetailsReport };
            OnDependency<IReportingRepository>()
                .Setup(x => x.GetByExample<ClientDetailsReport>(It.IsAny<object>()))
                .Returns(_clientDetailsReports);
        }

        protected override void Given()
        {
            Presenter.SetClient(new ClientReport(_clientId, "Client Name"));
            Presenter.Display();
        }

        protected override void When()
        {
            On<IClientDetailsView>().FireEvent(x => x.OnInitiateClientPhoneNumberChanged += null);
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
        public void Then_the_name_change_panel_will_be_enabled()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.EnablePhoneNumberPanel()).WasCalled();
        }
    }

    public class When_inserting_the_new_client_phone_number : PresenterTestFixture<ClientDetailsPresenter>
    {
        private readonly Guid _clientId = Guid.NewGuid();
        private ClientDetailsReport _clientDetailsReport;
        private List<ClientDetailsReport> _clientDetailsReports;

        protected override void SetupDependencies()
        {
            _clientDetailsReport = new ClientDetailsReport(_clientId, "Client Name", "Street", "123", "5000", "Bergen", "1234567890");
            _clientDetailsReports = new List<ClientDetailsReport> { _clientDetailsReport };
            OnDependency<IReportingRepository>()
                .Setup(x => x.GetByExample<ClientDetailsReport>(It.IsAny<object>()))
                .Returns(_clientDetailsReports);
        }

        protected override void Given()
        {
            Presenter.SetClient(new ClientReport(_clientId, "Client Name"));
            Presenter.Display();
            On<IClientDetailsView>().ValueFor(x => x.ClientName).IsSetTo("Client name");
            On<IClientDetailsView>().ValueFor(x => x.PhoneNumber).IsSetTo("1234567890");
            On<IClientDetailsView>().ValueFor(x => x.Street).IsSetTo("Street");
            On<IClientDetailsView>().ValueFor(x => x.StreetNumber).IsSetTo("123");
            On<IClientDetailsView>().ValueFor(x => x.PostalCode).IsSetTo("5000");
            On<IClientDetailsView>().ValueFor(x => x.City).IsSetTo("Bergen");
            On<IClientDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
            On<IClientDetailsView>().FireEvent(x => x.OnInitiateClientPhoneNumberChanged += null);
        }

        protected override void When()
        {
            On<IClientDetailsView>().ValueFor(x => x.PhoneNumber).IsSetTo("0987654321");
            On<IClientDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
        }

        [Then]
        public void Then_the_save_button_will_be_disabled()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.DisableSaveButton()).WasCalled();
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
        public void Then_the_save_button_will_be_enabled()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.EnableSaveButton()).WasCalled();
        }
    }

    public class When_clearing_the_new_client_phone_number : PresenterTestFixture<ClientDetailsPresenter>
    {
        private readonly Guid _clientId = Guid.NewGuid();
        private ClientDetailsReport _clientDetailsReport;
        private List<ClientDetailsReport> _clientDetailsReports;

        protected override void SetupDependencies()
        {
            _clientDetailsReport = new ClientDetailsReport(_clientId, "Client Name", "Street", "123", "5000", "Bergen", "1234567890");
            _clientDetailsReports = new List<ClientDetailsReport> { _clientDetailsReport };
            OnDependency<IReportingRepository>()
                .Setup(x => x.GetByExample<ClientDetailsReport>(It.IsAny<object>()))
                .Returns(_clientDetailsReports);
        }

        protected override void Given()
        {
            Presenter.SetClient(new ClientReport(_clientId, "Client Name"));
            Presenter.Display();
            On<IClientDetailsView>().ValueFor(x => x.ClientName).IsSetTo("Client name");
            On<IClientDetailsView>().ValueFor(x => x.PhoneNumber).IsSetTo("1234567890");
            On<IClientDetailsView>().ValueFor(x => x.Street).IsSetTo("Street");
            On<IClientDetailsView>().ValueFor(x => x.StreetNumber).IsSetTo("123");
            On<IClientDetailsView>().ValueFor(x => x.PostalCode).IsSetTo("5000");
            On<IClientDetailsView>().ValueFor(x => x.City).IsSetTo("Bergen");
            On<IClientDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
            On<IClientDetailsView>().FireEvent(x => x.OnInitiateClientPhoneNumberChanged += null);
        }

        protected override void When()
        {
            On<IClientDetailsView>().ValueFor(x => x.PhoneNumber).IsSetTo("1234567890");
            On<IClientDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
        }

        [Then]
        public void Then_the_save_button_will_be_enabled()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.DisableSaveButton()).WasCalled();
        }
    }

    public class When_saving_the_new_client_phone_number : PresenterTestFixture<ClientDetailsPresenter>
    {
        private readonly Guid _clientId = Guid.NewGuid();
        private ClientDetailsReport _clientDetailsReport;
        private List<ClientDetailsReport> _clientDetailsReports;

        protected override void SetupDependencies()
        {
            OnDependency<IPopupPresenter>()
                .Setup(x => x.CatchPossibleException(It.IsAny<Action>()))
                .Callback<Action>(x => x());

            _clientDetailsReport = new ClientDetailsReport(_clientId, "Client Name", "Street", "123", "5000", "Bergen", "1234567890");
            _clientDetailsReports = new List<ClientDetailsReport> { _clientDetailsReport };
            OnDependency<IReportingRepository>()
                .Setup(x => x.GetByExample<ClientDetailsReport>(It.IsAny<object>()))
                .Returns(_clientDetailsReports);
        }

        protected override void Given()
        {
            Presenter.SetClient(new ClientReport(_clientId, "Client Name"));
            Presenter.Display();
            On<IClientDetailsView>().ValueFor(x => x.ClientName).IsSetTo("Client name");
            On<IClientDetailsView>().ValueFor(x => x.PhoneNumber).IsSetTo("0987654321");
            On<IClientDetailsView>().ValueFor(x => x.Street).IsSetTo("Street");
            On<IClientDetailsView>().ValueFor(x => x.StreetNumber).IsSetTo("123");
            On<IClientDetailsView>().ValueFor(x => x.PostalCode).IsSetTo("5000");
            On<IClientDetailsView>().ValueFor(x => x.City).IsSetTo("Bergen");
            On<IClientDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
            On<IClientDetailsView>().FireEvent(x => x.OnInitiateClientPhoneNumberChanged += null);
        }

        protected override void When()
        {
            On<IClientDetailsView>().FireEvent(x => x.OnSaveNewPhoneNumber += null);
        }

        [Then]
        public void Then_a_change_client_phone_number_command_will_be_published()
        {
            On<ICommandBus>().VerifyThat.Method(x => x.Publish(It.IsAny<ChangeClientPhoneNumberCommand>())).WasCalled();
        }

        [Then]
        public void Then_the_save_button_will_be_disabled()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.DisableSaveButton()).WasCalled();
        }

        [Then]
        public void Then_the_menu_button_will_be_enabled()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.EnableAddNewAccountMenu()).WasCalled();
            On<IClientDetailsView>().VerifyThat.Method(x => x.EnableClientHasMovedMenu()).WasCalled();
            On<IClientDetailsView>().VerifyThat.Method(x => x.EnableNameChangedMenu()).WasCalled();
            On<IClientDetailsView>().VerifyThat.Method(x => x.EnablePhoneNumberChangedMenu()).WasCalled();
        }

        [Then]
        public void Then_the_details_panel_will_be_enabled()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.EnableOverviewPanel()).WasCalled();
        }
    }

    public class When_changing_the_phone_number_of_a_client : CommandTestFixture<ChangeClientPhoneNumberCommand, ChangeClientPhoneNumberCommandHandler, Client>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new ClientCreatedEvent(Guid.NewGuid(), "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937")).ToVersion(1);
        }

        protected override ChangeClientPhoneNumberCommand When()
        {
            return new ChangeClientPhoneNumberCommand(Guid.NewGuid(), "95009937");
        }

        [Then]
        public void Then_a_client_phone_number_changed_event_will_be_published()
        {
            PublishedEvents.Last().WillBeOfType<ClientPhoneNumberChangedEvent>();
        }

        [Then]
        public void Then_the_published_event_will_contain_the_new_phone_number_of_the_client()
        {
            PublishedEvents.Last<ClientPhoneNumberChangedEvent>().PhoneNumber.WillBe("95009937");
        }
    }

    public class When_changing_the_phone_number_of_a_not_yet_created_client : CommandTestFixture<ChangeClientPhoneNumberCommand, ChangeClientPhoneNumberCommandHandler, Client>
    {
        protected override ChangeClientPhoneNumberCommand When()
        {
            return new ChangeClientPhoneNumberCommand(Guid.NewGuid(), "95009937");
        }

        [Then]
        public void Then_a_client_was_not_created_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<ClientWasNotCreatedException>();
        }

        [Then]
        public void Then_the_exception_message_will_be()
        {
            CaughtException.Message.WillBe("The Client is not created and no opperations can be executed on it");
        }
    }

    public class When_the_phone_number_of_a_client_was_changed : EventTestFixture<ClientPhoneNumberChangedEvent, ClientPhoneNumberChangedEventHandler>
    {
        private static Guid _clientId;
        private object UpdateObject;
        private object WhereObject;

        protected override void SetupDependencies()
        {
            OnDependency<IReportingRepository>()
                .Setup(x => x.Update<ClientDetailsReport>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { UpdateObject = u; WhereObject = w; });
        }

        protected override ClientPhoneNumberChangedEvent When()
        {
            var clientPhoneNumberWasChangedEvent = new ClientPhoneNumberChangedEvent("1234567890") { AggregateId = Guid.NewGuid() };
            _clientId = clientPhoneNumberWasChangedEvent.AggregateId;
            return clientPhoneNumberWasChangedEvent;
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_update_the_client_details_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Update<ClientDetailsReport>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [Then]
        public void Then_the_client_details_report_will_be_updated_with_the_expected_details()
        {
            UpdateObject.WillBeSimuliar(new { PhoneNumber = "1234567890" }.ToString());
            WhereObject.WillBeSimuliar(new { Id = _clientId });
        }
    }
}