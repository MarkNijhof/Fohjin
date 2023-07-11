using Fohjin.DDD.Bus.Direct;
using Fohjin.DDD.Configuration;
using Fohjin.DDD.EventHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Test.Fohjin.DDD.Bus
{
    public class When_multiple_events_gets_published_to_the_bus_containing_multiple_event_handlers : BaseTestFixture<DirectBus>
    {
        private FirstTestEventHandler _handler;
        private SecondTestEventHandler _secondHandler;
        private TestEvent _event;
        private TestEvent _otherEvent;

        protected override void SetupDependencies()
        {
            _handler = new FirstTestEventHandler();
            _secondHandler = new SecondTestEventHandler();
            Services.AddConfigurationServices()
                .AddTransient<IEventHandler>(_ => _handler)
                .AddTransient<IEventHandler>(_ => _secondHandler)
                ;

            var messageRouter = new MessageRouter(this.Provider, this.Logger<MessageRouter>());
            DoNotMock?.Add(typeof(IRouteMessages), messageRouter);
        }

        protected override void Given()
        {
            _event = new TestEvent();
            _otherEvent = new TestEvent();
        }

        protected override async Task WhenAsync()
        {
            if (SubjectUnderTest == null || _event == null || _otherEvent == null)
                return;

            SubjectUnderTest.Publish(new List<object> { _event, _otherEvent });
            await SubjectUnderTest.CommitAsync();
        }

        [TestMethod]
        public void Then_the_execute_method_on_the_first_returned_event_handler_is_invoked_with_the_first_provided_event()
        {
            _handler?.Ids[0].WillBe(_event?.Id);
        }

        [TestMethod]
        public void Then_the_execute_method_on_the_first_returned_event_handler_is_invoked_with_the_second_provided_event()
        {
            _handler?.Ids[1].WillBe(_otherEvent?.Id);
        }

        [TestMethod]
        public void Then_the_execute_method_on_the_second_returned_event_handler_is_invoked_with_the_first_provided_event()
        {
            _secondHandler?.Ids[0].WillBe(_event?.Id);
        }

        [TestMethod]
        public void Then_the_execute_method_on_the_second_returned_event_handler_is_invoked_with_the_second_provided_event()
        {
            _secondHandler?.Ids[1].WillBe(_otherEvent?.Id);
        }
    }
}