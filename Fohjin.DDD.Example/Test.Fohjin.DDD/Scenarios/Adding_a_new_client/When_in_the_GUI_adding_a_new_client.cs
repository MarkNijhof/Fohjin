using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Scenarios.Adding_a_new_client
{
    public class When_in_the_GUI_adding_a_new_client : PresenterTestFixture<ClientSearchFormPresenter>
    {
        protected override void When()
        {
            On<IClientSearchFormView>().FireEvent(x => x.OnCreateNewClient += delegate { });
        }

        [TestMethod]
        public void Then_client_report_data_from_the_reporting_repository_is_being_loaded_into_the_view()
        {
            On<IClientDetailsPresenter>().VerifyThat.Method(x => x.SetClient(null)).WasCalled();
        }

        [TestMethod]
        public void Then_display_will_be_called_on_the_view()
        {
            On<IClientDetailsPresenter>().VerifyThat.Method(x => x.Display()).WasCalled();
        }
    }
}