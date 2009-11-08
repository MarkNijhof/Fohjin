using System;
using System.Collections.Generic;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dto;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Client_wants_to_open_a_new_account
{
    public class When_in_the_GUI_saving_the_new_account : PresenterTestFixture<ClientDetailsPresenter>
    {
        protected override void SetupDependencies()
        {
            OnDependency<IPopupPresenter>()
                .Setup(x => x.CatchPossibleException(It.IsAny<Action>()))
                .Callback<Action>(x => x());

            OnDependency<IReportingRepository>()
                .Setup(x => x.GetByExample<ClientDetailsReport>(It.IsAny<object>()))
                .Returns(new List<ClientDetailsReport> { new ClientDetailsReport(Guid.NewGuid(), "Client Name", "street", "123", "5000", "bergen", "1234567890") });
        }

        protected override void Given()
        {
            Presenter.SetClient(new ClientReport(Guid.NewGuid(), "Client name"));
            Presenter.Display();
            On<IClientDetailsView>().FireEvent(x => x.OnInitiateOpenNewAccount += delegate { });
            On<IClientDetailsView>().ValueFor(x => x.NewAccountName).IsSetTo("New account name");
            On<IClientDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
        }

        protected override void When()
        {
            On<IClientDetailsView>().FireEvent(x => x.OnCreateNewAccount += null);
        }

        [Then]
        public void Then_a_add_new_account_to_client_command_will_be_published()
        {
            On<IBus>().VerifyThat.Method(x => x.Publish(It.IsAny<OpenNewAccountForClientCommand>())).WasCalled();
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
        public void Then_overview_panel_will_be_shown()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.EnableOverviewPanel()).WasCalled();
        }
    }
}