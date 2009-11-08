using System;
using System.Collections.Generic;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dto;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Client_wants_to_open_a_new_account
{
    public class When_in_the_GUI_opening_a_new_account : PresenterTestFixture<ClientDetailsPresenter>
    {
        protected override void SetupDependencies()
        {
            OnDependency<IReportingRepository>()
                .Setup(x => x.GetByExample<ClientDetailsReport>(It.IsAny<object>()))
                .Returns(new List<ClientDetailsReport> { new ClientDetailsReport(Guid.NewGuid(), "Client Name", "street", "123", "5000", "bergen", "1234567890") });
        }

        protected override void When()
        {
            Presenter.SetClient(new ClientReport(Guid.NewGuid(), "Client name"));
            Presenter.Display();
            On<IClientDetailsView>().FireEvent(x => x.OnInitiateOpenNewAccount += delegate { });
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
        public void Then_the_add_new_panel_will_be_enabled()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.EnableAddNewAccountPanel()).WasCalled();
        }
    }
}