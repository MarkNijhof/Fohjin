using System;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Reporting.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Displaying_account_details
{
    public class When_in_the_GUI_opening_an_existing_account : PresenterTestFixture<ClientDetailsPresenter>
    {
        private AccountReport? _accountReport;

        protected override void SetupDependencies()
        {
            OnDependency<IPopupPresenter>()
                .Setup(x => x.CatchPossibleException(It.IsAny<Action>()))
                .Callback<Action>(x => x());

            _accountReport = new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "Account name", "1234567890");

            OnDependency<IClientDetailsView>()
                .Setup(x => x.GetSelectedAccount())
                .Returns(_accountReport);
        }

        protected override void When()
        {
            On<IClientDetailsView>().FireEvent(x => x.OnOpenSelectedAccount += delegate { });
        }

        [TestMethod]
        public void Then_get_selected_account_will_be_requested_from_th_view()
        {
            On<IClientDetailsView>().VerifyThat.Method(x => x.GetSelectedAccount()).WasCalled();
        }

        [TestMethod]
        public void Then_client_report_data_from_the_reporting_repository_is_being_loaded_into_the_view()
        {
            On<IAccountDetailsPresenter>().VerifyThat.Method(x => x.SetAccount(_accountReport)).WasCalled();
        }

        [TestMethod]
        public void Then_display_will_be_called_on_the_view()
        {
            On<IAccountDetailsPresenter>().VerifyThat.Method(x => x.Display()).WasCalled();
        }
    }
}