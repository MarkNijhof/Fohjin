using System;
using System.Collections.Generic;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dto;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Depositing_cash
{
    public class When_in_the_GUI_making_a_cash_deposite : PresenterTestFixture<AccountDetailsPresenter>
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
        }

        protected override void Given()
        {
            Presenter.SetAccount(new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "Account name", "1234567890"));
            Presenter.Display();
        }

        protected override void When()
        {
            On<IAccountDetailsView>().FireEvent(x => x.OnInitiateMoneyDeposite += null);
        }

        [Then]
        public void Then_the_current_amount_is_set_to_zero()
        {
            On<IAccountDetailsView>().VerifyThat.ValueIsSetFor(x => x.DepositeAmount = 0M);
        }

        [Then]
        public void Then_the_save_button_will_be_disabled()
        {
            On<IAccountDetailsView>().VerifyThat.Method(x => x.DisableMenuButtons()).WasCalled();
        }

        [Then]
        public void Then_the_deposite_panel_will_be_enabled()
        {
            On<IAccountDetailsView>().VerifyThat.Method(x => x.EnableDepositePanel()).WasCalled();
        }
    }
}