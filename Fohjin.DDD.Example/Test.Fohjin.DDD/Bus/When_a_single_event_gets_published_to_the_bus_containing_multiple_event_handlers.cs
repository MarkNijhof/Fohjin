﻿using Fohjin.DDD.Bus.Direct;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Bus
{
    public class When_a_single_event_gets_published_to_the_bus_containing_multiple_event_handlers : BaseTestFixture<DirectBus>
    {
        private FirstTestEventHandler _handler;
        private SecondTestEventHandler _secondHandler;
        private TestEvent _event;

        protected override void SetupDependencies()
        {
            _handler = new FirstTestEventHandler();
            _secondHandler = new SecondTestEventHandler();
            var messageRouter = new MessageRouter(this.Provider, this.Logger<MessageRouter>());
            messageRouter.Register<TestEvent>(x => _handler.Execute(x));
            messageRouter.Register<TestEvent>(x => _secondHandler.Execute(x));
            DoNotMock.Add(typeof(IRouteMessages), messageRouter);
        }

        protected override void Given()
        {
            _event = new TestEvent();
        }

        protected override void When()
        {
            SubjectUnderTest.Publish(new List<object> { _event });
            SubjectUnderTest.Commit();
        }

        [TestMethod]
        public void Then_the_execute_method_on_the_first_returned_event_handler_is_invoked_with_the_first_provided_event()
        {
            _handler.Ids.First().WillBe(_event.Id);
        }

        [TestMethod]
        public void Then_the_execute_method_on_the_second_returned_event_handler_is_invoked_with_the_first_provided_event()
        {
            _secondHandler.Ids.First().WillBe(_event.Id);
        }
    }
}