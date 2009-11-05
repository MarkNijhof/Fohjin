using System;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Bus.Implementation;
using Fohjin.DDD.Commands;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Adding_a_new_client
{
    public class When_in_the_GUI_the_phone_number_of_the_new_client_is_saved : PresenterTestFixture<ClientDetailsPresenter>
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