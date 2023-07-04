using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Reporting.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Displaying_client_details
{
    public class When_in_the_GUI_opening_an_existing_client : PresenterTestFixture<ClientSearchFormPresenter>
    {
        private ClientReport _clientReport = new();

        protected override void SetupDependencies()
        {
            OnDependency<IPopupPresenter>()
                ?.Setup(x => x.CatchPossibleException(It.IsAny<Action>()))
                .Callback<Action>(x => x());

            _clientReport = new ClientReport(Guid.NewGuid(), "Client Name");

            OnDependency<IClientSearchFormView>()
                ?.Setup(x => x.GetSelectedClient())
                .Returns(_clientReport);
        }

        protected override void When()
        {
            On<IClientSearchFormView>().FireEvent(x => x.OnOpenSelectedClient += delegate { });
        }

        [TestMethod]
        public void Then_get_selected_client_will_be_called_on_the_view()
        {
            On<IClientSearchFormView>().VerifyThat.Method(x => x.GetSelectedClient()).WasCalled();
        }

        [TestMethod]
        public void Then_client_report_data_from_the_reporting_repository_is_being_loaded_into_the_view()
        {
            On<IClientDetailsPresenter>().VerifyThat.Method(x => x.SetClient(_clientReport)).WasCalled();
        }

        [TestMethod]
        public void Then_display_will_be_called_on_the_view()
        {
            On<IClientDetailsPresenter>().VerifyThat.Method(x => x.Display()).WasCalled();
        }
    }
}