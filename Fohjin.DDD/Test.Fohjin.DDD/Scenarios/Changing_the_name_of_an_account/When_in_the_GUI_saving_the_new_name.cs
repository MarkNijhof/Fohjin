using System;
using System.Collections.Generic;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dto;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Changing_the_name_of_an_account
{
    public class When_in_the_GUI_saving_the_new_name : PresenterTestFixture<AccountDetailsPresenter>
    {
        protected override void SetupDependencies()
        {
            OnDependency<IPopupPresenter>()
                .Setup(x => x.CatchPossibleException(It.IsAny<Action>()))
                .Callback<Action>(x => x());

            var accountDetailsReports = new List<AccountDetailsReport> { new AccountDetailsReport(Guid.NewGuid(), Guid.NewGuid(), "Account name", 10.5M, "1234567890") };
            OnDependency<IReportingRepository>()
                .Setup(x => x.GetByExample<AccountDetailsReport>(It.IsAny<object>()))
                .Returns(accountDetailsReports);

            var accountReports = new List<AccountReport> { new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "Account name 1", "1234567890") };
            OnDependency<IReportingRepository>()
                .Setup(x => x.GetByExample<AccountReport>(It.IsAny<object>()))
                .Returns(accountReports);
        }

        protected override void Given()
        {
            Presenter.SetAccount(new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "Account name", "1234567890"));
            Presenter.Display();
            On<IAccountDetailsView>().FireEvent(x => x.OnInitiateAccountNameChange += null);
            On<IAccountDetailsView>().ValueFor(x => x.AccountName).IsSetTo("New Account name");
            On<IAccountDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
        }

        protected override void When()
        {
            On<IAccountDetailsView>().FireEvent(x => x.OnChangeAccountName += null);
        }

        [Then]
        public void Then_a_change_account_name_command_will_be_published()
        {
            On<IBus>().VerifyThat.Method(x => x.Publish(It.IsAny<ChangeAccountNameCommand>())).WasCalled();
        }

        [Then]
        public void Then_the_menu_button_will_be_enabled()
        {
            On<IAccountDetailsView>().VerifyThat.Method(x => x.EnableMenuButtons()).WasCalled();
        }

        [Then]
        public void Then_the_details_panel_will_be_enabled()
        {
            On<IAccountDetailsView>().VerifyThat.Method(x => x.EnableDetailsPanel()).WasCalled();
        }
    }
}