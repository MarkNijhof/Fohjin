using System;
using System.Reflection;
using Castle.Core.Interceptor;
using Castle.DynamicProxy;
using Fohjin.EventStore.Configuration;

namespace Fohjin.EventStore.Infrastructure
{
    public interface IAggregateRootFactory
    {
        TAggregateRoot Create<TAggregateRoot>();
        object Create(Type entityType);
    }

    public class AggregateRootFactory : IAggregateRootFactory
    {
        private readonly EventProcessorCache _eventProcessorCache;
        private readonly ApprovedEntitiesCache _approvedEntitiesCache;
        private readonly ProxyGenerator _proxyGenerator;

        public AggregateRootFactory(EventProcessorCache eventProcessorCache, ApprovedEntitiesCache approvedEntitiesCache)
        {
            _eventProcessorCache = eventProcessorCache;
            _approvedEntitiesCache = approvedEntitiesCache;
            _proxyGenerator = new ProxyGenerator();
        }

        public TAggregateRoot Create<TAggregateRoot>()
        {
            return (TAggregateRoot)Create(typeof(TAggregateRoot));
        }

        public object Create(Type entityType)
        {
            HasApplyMethod(entityType);

            var eventProvider = new EventProvider(entityType, _eventProcessorCache);
            var orginator = new Orginator();
            
            return CreateProxy(entityType, eventProvider, orginator);
        }

        private object CreateProxy(Type entityType, IInterceptor eventProvider, Orginator orginator)
        {
            var proxyGenerationOptions = new ProxyGenerationOptions();
            proxyGenerationOptions.AddMixinInstance(eventProvider);
            proxyGenerationOptions.AddMixinInstance(orginator);

            return _proxyGenerator.CreateClassProxy(
                    entityType, 
                    proxyGenerationOptions,
                    eventProvider
                );
        }

        private void HasApplyMethod(Type entityType)
        {
            if (_approvedEntitiesCache.IsEntityApproved(entityType))
                return;

            var applyMethod = entityType.GetMethod("Apply", BindingFlags.Instance | BindingFlags.NonPublic);

            if (applyMethod == null || applyMethod.ToString() != "Void Apply(System.Object)")
                throw new MethodMissingException(string.Format("Object '{0}' needs to have a 'proteced virtual void Apply(object @event)' method declared", entityType.FullName));

            _approvedEntitiesCache.RegisterApprovedEntity(entityType);
        }
    }
}