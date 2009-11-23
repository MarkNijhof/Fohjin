using System;
using System.Collections.Generic;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dto;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Client_wants_to_close_an_account
{
    public class When_in_the_GUI_closing_an_account : PresenterTestFixture<AccountDetailsPresenter>
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
        }

        protected override void When()
        {
            On<IAccountDetailsView>().FireEvent(x => x.OnCloseTheAccount += null);
        }

        [Then]
        public void Then_a_close_account_command_gets_send_to_the_bus()
        {
            On<IBus>().VerifyThat.Method(x => x.Publish(It.IsAny<CloseAccountCommand>())).WasCalled();
        }

        [Then]
        public void Then_the_view_will_be_closed()
        {
            On<IAccountDetailsView>().VerifyThat.Method(x => x.Close()).WasCalled();
        }
    }
}