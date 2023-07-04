using Fohjin.DDD.Bus.Direct;
using Fohjin.DDD.Configuration;
using Fohjin.DDD.EventHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Bus
{
    public class When_a_single_event_gets_published_to_the_bus_containing_an_sinlge_event_handler : BaseTestFixture<DirectBus>
    {
        private FirstTestEventHandler _handler;
        private TestEvent _event;

        protected override void SetupDependencies()
        {
            _handler = new FirstTestEventHandler();
            Services.AddConfigurationServices()
                .AddTransient<IEventHandler>(_ => _handler)
                ;
            ;
            var messageRouter = new MessageRouter(this.Provider, this.Logger<MessageRouter>());
            DoNotMock?.Add(typeof(IRouteMessages), messageRouter);
        }

        protected override void Given()
        {
            _event = new TestEvent();
        }

        protected override async Task WhenAsync()
        {
            if (SubjectUnderTest == null || _event == null)
                return;

            SubjectUnderTest.Publish(new List<object> { _event });
            await SubjectUnderTest.CommitAsync();
        }

        [TestMethod]
        public void Then_the_execute_method_on_the_returned_event_handler_is_invoked_with_the_provided_event()
        {
            _handler?.Ids.First().WillBe(_event?.Id);
        }
    }
}