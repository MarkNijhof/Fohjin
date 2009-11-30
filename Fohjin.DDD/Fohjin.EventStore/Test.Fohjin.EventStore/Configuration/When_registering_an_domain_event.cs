using System.Collections.Generic;
using System.Linq;
using Fohjin.EventStore.Configuration;
using Fohjin.EventStore.Reflection;

namespace Test.Fohjin.EventStore.Configuration
{
    public class When_registering_an_domain_event : BaseTestFixture<PreProcessor>
    {
        private readonly EventProcessorCache EventProcessorCache = new EventProcessorCache();
        private readonly EventAccessor _eventAccessor = new EventAccessor(new EventPropertyLocator());

        protected override void SetupDependencies()
        {
            ActualImplementation.Add(typeof(EventProcessorCache), EventProcessorCache);
            ActualImplementation.Add(typeof(EventAccessor), _eventAccessor);
        }

        protected override void When()
        {
            SubjectUnderTest.RegisterForPreProcessing<ClientMovedEvent>();
            SubjectUnderTest.Process();
        }

        [Then]
        public void Then_the_event_processors_for_client_moved_event_will_be_registered()
        {
            IEnumerable<EventProcessor> eventProcessors;
            EventProcessorCache.TryGetEventProcessorsFor(typeof(ClientMovedEvent), out eventProcessors);
            eventProcessors.Count().WillBe(1);
        }

        [Then]
        public void Then_the_event_processors_for_client_moved_event_will_be_registered_with_the_right_information()
        {
            IEnumerable<EventProcessor> eventProcessors;
            EventProcessorCache.TryGetEventProcessorsFor(typeof (ClientMovedEvent), out eventProcessors);
            eventProcessors.Last().RegisteredEvent.WillBe(typeof(ClientMovedEvent));
            eventProcessors.Last().Property.Name.WillBe("Address");
        }
    }
}