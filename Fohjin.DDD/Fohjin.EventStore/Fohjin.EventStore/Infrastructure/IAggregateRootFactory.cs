using System;
using System.Reflection;
using Castle.Core.Interceptor;
using Castle.DynamicProxy;

namespace Fohjin.EventStore.Infrastructure
{
    public interface IAggregateRootFactory
    {
        TAggregateRoot Create<TAggregateRoot>();
        object Create(Type type);
    }

    public class AggregateRootFactory : IAggregateRootFactory
    {
        private readonly IEventRegistrator _eventRegistrator;
        private readonly ICacheRegisteredEvents _cacheRegisteredEvents;
        private readonly ProxyGenerator _proxyGenerator;

        public AggregateRootFactory(IEventRegistrator eventRegistrator, ICacheRegisteredEvents cacheRegisteredEvents)
        {
            _eventRegistrator = eventRegistrator;
            _cacheRegisteredEvents = cacheRegisteredEvents;
            _proxyGenerator = new ProxyGenerator();
        }

        public TAggregateRoot Create<TAggregateRoot>()
        {
            return (TAggregateRoot)Create(typeof(TAggregateRoot));
        }

        public object Create(Type type)
        {
            HasApplyMethod(type);
            HasRegiteredEventsMethod(type);

            var eventProvider = new EventProvider(type, _eventRegistrator, _cacheRegisteredEvents);
            var orginator = new Orginator();
            
            var proxy = CreateProxy(type, eventProvider, orginator);

            orginator.SetProxy(proxy);

            eventProvider.RegisterEventHandlers(proxy);

            return proxy;
        }

        private object CreateProxy(Type type, IInterceptor eventProvider, Orginator orginator)
        {
            var proxyGenerationOptions = new ProxyGenerationOptions();
            proxyGenerationOptions.AddMixinInstance(eventProvider);
            proxyGenerationOptions.AddMixinInstance(orginator);

            return _proxyGenerator.CreateClassProxy(
                    type, 
                    proxyGenerationOptions,
                    eventProvider
                );
        }

        private static void HasApplyMethod(Type type)
        {
            var applyMethod = type.GetMethod("Apply", BindingFlags.Instance | BindingFlags.NonPublic);

            if (applyMethod == null || applyMethod.ToString() != "Void Apply(System.Object)")
                throw new ProtectedApplyMethodMissingException(string.Format("Object '{0}' needs to have a 'proteced virtual void Apply(object @event)' method declared", type.FullName));
        }

        private static void HasRegiteredEventsMethod(Type type)
        {
            var regiteredEventsMethod = type.GetMethod("RegisteredEvents", BindingFlags.Static | BindingFlags.NonPublic);

            if (regiteredEventsMethod == null || regiteredEventsMethod.ToString() != "System.Collections.Generic.IEnumerable`1[System.Type] RegisteredEvents()")
                throw new ProtectedRegisteredEventsMethodMissingException(string.Format("Object '{0}' needs to have a 'protected IEnumerable<Type> RegisteredEvents()' method declared", type.FullName));
        }
    }
}