using System;
using System.Collections.Generic;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dto;

namespace Test.Fohjin.DDD.Scenarios.Opening_the_bank_application
{
    public class When_in_the_GUI_openeing_the_bank_application : PresenterTestFixture<ClientSearchFormPresenter>
    {
        private List<ClientReport> _clientReports;

        protected override void SetupDependencies()
        {
            _clientReports = new List<ClientReport> { new ClientReport(Guid.NewGuid(), "Client Name") };
            OnDependency<IReportingRepository>()
                .Setup(x => x.GetByExample<ClientReport>(null))
                .Returns(_clientReports);
        }

        protected override void When()
        {
            Presenter.Display();
        }

        [Then]
        public void Then_show_dialog_will_be_called_on_the_view()
        {
            On<IClientSearchFormView>().VerifyThat.Method(x => x.ShowDialog()).WasCalled();
        }

        [Then]
        public void Then_client_report_data_from_the_reporting_repository_is_being_loaded_into_the_view()
        {
            On<IClientSearchFormView>().VerifyThat.ValueIsSetFor(x => x.Clients = _clientReports);
        }
    }
}