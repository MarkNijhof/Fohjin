using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.Bus.Direct;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events;

namespace Test.Fohjin.DDD.Bus
{
    public class When_a_single_event_gets_published_to_the_bus_containing_an_sinlge_event_handler : BaseTestFixture<DirectBus>
    {
        private FirstTestEventHandler _handler;
        private TestEvent _event;

        protected override void SetupDependencies()
        {
            _handler = new FirstTestEventHandler();
            var messageRouter = new MessageRouter();
            messageRouter.Register<TestEvent>(x => _handler.Execute(x));
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

        [Then]
        public void Then_the_execute_method_on_the_returned_event_handler_is_invoked_with_the_provided_event()
        {
            _handler.Ids.First().WillBe(_event.Id);
        }
    }

    public class When_a_single_event_gets_published_to_the_bus_containing_multiple_event_handlers : BaseTestFixture<DirectBus>
    {
        private FirstTestEventHandler _handler;
        private SecondTestEventHandler _secondHandler;
        private TestEvent _event;

        protected override void SetupDependencies()
        {
            _handler = new FirstTestEventHandler();
            _secondHandler = new SecondTestEventHandler();
            var messageRouter = new MessageRouter();
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

        [Then]
        public void Then_the_execute_method_on_the_first_returned_event_handler_is_invoked_with_the_first_provided_event()
        {
            _handler.Ids.First().WillBe(_event.Id);
        }

        [Then]
        public void Then_the_execute_method_on_the_second_returned_event_handler_is_invoked_with_the_first_provided_event()
        {
            _secondHandler.Ids.First().WillBe(_event.Id);
        }
    }

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
            var messageRouter = new MessageRouter();
            messageRouter.Register<TestEvent>(x => _handler.Execute(x));
            messageRouter.Register<TestEvent>(x => _secondHandler.Execute(x));
            DoNotMock.Add(typeof(IRouteMessages), messageRouter);
        }

        protected override void Given()
        {
            _event = new TestEvent();
            _otherEvent = new TestEvent();
        }

        protected override void When()
        {
            SubjectUnderTest.Publish(new List<object> { _event, _otherEvent });
            SubjectUnderTest.Commit();
        }

        [Then]
        public void Then_the_execute_method_on_the_first_returned_event_handler_is_invoked_with_the_first_provided_event()
        {
            _handler.Ids[0].WillBe(_event.Id);
        }

        [Then]
        public void Then_the_execute_method_on_the_first_returned_event_handler_is_invoked_with_the_second_provided_event()
        {
            _handler.Ids[1].WillBe(_otherEvent.Id);
        }

        [Then]
        public void Then_the_execute_method_on_the_second_returned_event_handler_is_invoked_with_the_first_provided_event()
        {
            _secondHandler.Ids[0].WillBe(_event.Id);
        }

        [Then]
        public void Then_the_execute_method_on_the_second_returned_event_handler_is_invoked_with_the_second_provided_event()
        {
            _secondHandler.Ids[1].WillBe(_otherEvent.Id);
        }
    }

    public class TestEvent : DomainEvent
    {
    }

    public class FirstTestEventHandler : IEventHandler<TestEvent>
    {
        public List<Guid> Ids;

        public FirstTestEventHandler()
        {
            Ids = new List<Guid>();
        }

        public void Execute(TestEvent command)
        {
            Ids.Add(command.Id);
        }
    }

    public class SecondTestEventHandler : IEventHandler<TestEvent>
    {
        public List<Guid> Ids;

        public SecondTestEventHandler()
        {
            Ids = new List<Guid>();
        }

        public void Execute(TestEvent command)
        {
            Ids.Add(command.Id);
        }
    }
}