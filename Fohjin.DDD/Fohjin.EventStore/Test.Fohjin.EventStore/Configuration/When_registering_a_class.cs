using System.Linq;
using Fohjin.EventStore.Configuration;
using Fohjin.EventStore.Reflection;

namespace Test.Fohjin.EventStore.Configuration
{
    public class When_registering_a_class : BaseTestFixture<PreProcessor>
    {
        private readonly EventProcessorCache EventProcessorCache = new EventProcessorCache();
        private readonly EventAccessor _eventAccessor = new EventAccessor(new DomainEventPropertyLocator());

        protected override void SetupDependencies()
        {
            ActualImplementation.Add(typeof(EventProcessorCache), EventProcessorCache);
            ActualImplementation.Add(typeof(EventAccessor), _eventAccessor);
        }

        protected override void When()
        {
            SubjectUnderTest.RegisterForProcessing<TestClient>();
            SubjectUnderTest.Process();
        }

        [Then]
        public void Then_the_registered_class_will_be_scanned_for_registered_events()
        {
            EventProcessorCache.GetEventsFor(typeof (TestClient)).Count().WillBe(1);
        }

        [Then]
        public void Then_the_client_moved_event_will_be_registered()
        {
            EventProcessorCache.GetEventsFor(typeof(TestClient)).Last().WillBe(typeof(ClientMovedEvent));
        }

        [Then]
        public void Then_the_event_processors_for_client_moved_event_will_be_registered()
        {
            EventProcessorCache.GetEventProcessorsFor(typeof(TestClient), typeof(ClientMovedEvent)).Count().WillBe(1);
        }

        [Then]
        public void Then_the_event_processors_for_client_moved_event_will_be_registered_with_the_right_information()
        {
            EventProcessorCache.GetEventProcessorsFor(typeof(TestClient), typeof(ClientMovedEvent)).Last().RegisteredEvent.WillBe(typeof(ClientMovedEvent));
            EventProcessorCache.GetEventProcessorsFor(typeof(TestClient), typeof(ClientMovedEvent)).Last().Property.Name.WillBe("Address");
        }
    }
}