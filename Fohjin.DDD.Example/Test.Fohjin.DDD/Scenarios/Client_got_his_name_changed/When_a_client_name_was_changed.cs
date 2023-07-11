using System;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Client_got_his_name_changed
{
    public class When_a_client_name_was_changed : EventTestFixture<ClientNameChangedEvent, ClientNameChangedEventHandler>
    {
        private static Guid _clientId;
        private object? UpdateClientObject;
        private object? WhereClientObject;
        private object? UpdateClientDetailsObject;
        private object? WhereClientDetailsObject;

        protected override void SetupDependencies()
        {
            OnDependency<IReportingRepository>()
                ?.Setup(x => x.Update<ClientReport>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { UpdateClientObject = u; WhereClientObject = w; });

            OnDependency<IReportingRepository>()
                ?.Setup(x => x.Update<ClientDetailsReport>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { UpdateClientDetailsObject = u; WhereClientDetailsObject = w; });
        }

        protected override ClientNameChangedEvent When()
        {
            var clientNameWasChangedEvent = new ClientNameChangedEvent("New Client Name") { AggregateId = Guid.NewGuid() };
            _clientId = clientNameWasChangedEvent.AggregateId;
            return clientNameWasChangedEvent;
        }

        [TestMethod]
        public void Then_the_reporting_repository_will_be_used_to_update_the_client_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Update<ClientReport>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [TestMethod]
        public void Then_the_client_report_will_be_updated_with_the_expected_details()
        {
            UpdateClientObject.WillBeSimuliar(new { Name = "New Client Name" }.ToString() ?? "");
            WhereClientObject.WillBeSimuliar(new { Id = _clientId });
        }

        [TestMethod]
        public void Then_the_reporting_repository_will_be_used_to_update_the_client_details_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Update<ClientDetailsReport>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [TestMethod]
        public void Then_the_client_details_report_will_be_updated_with_the_expected_details()
        {
            UpdateClientDetailsObject.WillBeSimuliar(new { ClientName = "New Client Name" }.ToString() ?? "");
            WhereClientDetailsObject.WillBeSimuliar(new { Id = _clientId });
        }
    }
}