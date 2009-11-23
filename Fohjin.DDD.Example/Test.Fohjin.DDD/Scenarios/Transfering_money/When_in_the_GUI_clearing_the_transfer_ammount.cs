using System;
using System.Collections.Generic;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dto;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Transfering_money
{
    public class When_in_the_GUI_clearing_the_transfer_ammount : PresenterTestFixture<AccountDetailsPresenter>
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
            On<IAccountDetailsView>().ValueFor(x => x.AccountName).IsSetTo("Account name");
            On<IAccountDetailsView>().ValueFor(x => x.WithdrawlAmount).IsSetTo(0M);
            On<IAccountDetailsView>().ValueFor(x => x.DepositeAmount).IsSetTo(0M);
            On<IAccountDetailsView>().ValueFor(x => x.TransferAmount).IsSetTo(0M);
            On<IAccountDetailsView>().FireEvent(x => x.OnInitiateMoneyTransfer += null);
            On<IAccountDetailsView>().ValueFor(x => x.TransferAmount).IsSetTo(12.3M);
            On<IAccountDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
        }

        protected override void When()
        {
            On<IAccountDetailsView>().ValueFor(x => x.TransferAmount).IsSetTo(0M);
            On<IAccountDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
        }

        [Then]
        public void Then_the_save_button_will_be_enabled()
        {
            On<IAccountDetailsView>().VerifyThat.Method(x => x.DisableSaveButton()).WasCalled();
        }
    }
}