using System;
using System.Collections.Generic;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Reporting.Dto;
using Moq;

namespace Test.Fohjin.DDD.BankApplication.Presenters
{
    public class When_openeing_the_client_search_application : PresenterTestFixture<ClientSearchFormPresenter>
    {
        private List<ClientReport> _clientReports;

        protected override void SetupDependencies()
        {
            _clientReports = new List<ClientReport> {new ClientReport(Guid.NewGuid(), "Client Name")};
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

    public class When_opening_an_existing_client : PresenterTestFixture<ClientSearchFormPresenter>
    {
        private ClientReport _clientReport;

        protected override void SetupDependencies()
        {
            OnDependency<IPopupPresenter>()
                .Setup(x => x.CatchPossibleException(It.IsAny<Action>()))
                .Callback<Action>(x => x());

            _clientReport = new ClientReport(Guid.NewGuid(), "Client Name");

            OnDependency<IClientSearchFormView>()
                .Setup(x => x.GetSelectedClient())
                .Returns(_clientReport);
        }

        protected override void When()
        {
            On<IClientSearchFormView>().FireEvent(x => x.OnOpenSelectedClient += delegate { });
        }

        [Then]
        public void Then_get_selected_client_will_be_called_on_the_view()
        {
            On<IClientSearchFormView>().VerifyThat.Method(x => x.GetSelectedClient()).WasCalled();
        }

        [Then]
        public void Then_client_report_data_from_the_reporting_repository_is_being_loaded_into_the_view()
        {
            On<IClientDetailsPresenter>().VerifyThat.Method(x => x.SetClient(_clientReport)).WasCalled();
        }

        [Then]
        public void Then_display_will_be_called_on_the_view()
        {
            On<IClientDetailsPresenter>().VerifyThat.Method(x => x.Display()).WasCalled();
        }
    }

    public class When_creating_a_new_client : PresenterTestFixture<ClientSearchFormPresenter>
    {
        protected override void When()
        {
            On<IClientSearchFormView>().FireEvent(x => x.OnCreateNewClient += delegate { });
        }

        [Then]
        public void Then_client_report_data_from_the_reporting_repository_is_being_loaded_into_the_view()
        {
            On<IClientDetailsPresenter>().VerifyThat.Method(x => x.SetClient(null)).WasCalled();
        }

        [Then]
        public void Then_display_will_be_called_on_the_view()
        {
            On<IClientDetailsPresenter>().VerifyThat.Method(x => x.Display()).WasCalled();
        }
    }

    public class When_refreshing_the_client_search_application : PresenterTestFixture<ClientSearchFormPresenter>
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
            On<IClientSearchFormView>().FireEvent(x => x.OnRefresh += delegate { });
        }

        [Then]
        public void Then_client_report_data_from_the_reporting_repository_is_being_loaded_into_the_view()
        {
            On<IClientSearchFormView>().VerifyThat.ValueIsSetFor(x => x.Clients = _clientReports);
        }
    }
}