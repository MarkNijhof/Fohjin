using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.EventStore.Reflection;

namespace Fohjin.EventStore.Configuration
{
    public class PreProcessor
    {
        private readonly EventProcessorCache _eventProcessorCache;
        private readonly DomainEventLocator _domainEventLocator;
        private readonly EventAccessor _eventAccessor;
        private readonly List<Type> _registeredTypes;

        public PreProcessor(EventProcessorCache eventProcessorCache, DomainEventLocator domainEventLocator, EventAccessor eventAccessor)
        {
            _eventProcessorCache = eventProcessorCache;
            _domainEventLocator = domainEventLocator;
            _eventAccessor = eventAccessor;
            _registeredTypes = new List<Type>();
        }

        public void RegisterForProcessing<TEntity>()
        {
            RegisterForProcessing(typeof(TEntity));
        }

        public void RegisterForProcessing(Type entityType)
        {
            if (!_domainEventLocator.HasRequiredMethod(entityType))
                throw new MethodMissingException(string.Format("Object '{0}' needs to have a method declared to provide the domain events that the type can handle", entityType.FullName));

            _registeredTypes.Add(entityType);
        }

        public void Process()
        {
            _registeredTypes.ForEach(ProcessEntity);
        }

        private void ProcessEntity(Type entityType)
        {
            var RegisteredEvents = _domainEventLocator.RetrieveDomainEvents(entityType);
            foreach (var registeredEvent in RegisteredEvents)
            {
                var eventProcessors = _eventAccessor.BuildEventProcessors(registeredEvent);
                _eventProcessorCache.RegisterEventProcessors(entityType, registeredEvent, eventProcessors);
            }
        }
    }
}