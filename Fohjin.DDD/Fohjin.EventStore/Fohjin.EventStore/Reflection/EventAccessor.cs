using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Fohjin.EventStore.Configuration;

namespace Fohjin.EventStore.Reflection
{
    public class EventAccessor
    {
        private static readonly MethodInfo _createToInternalStateCopyLambdaMethod;
        private static readonly MethodInfo _createEventPropertyAccessorLabmdaMethod;

        static EventAccessor()
        {
            _createToInternalStateCopyLambdaMethod = typeof(EventAccessor).GetMethod("CreateToInternalStateCopyLambda", BindingFlags.Instance | BindingFlags.NonPublic);
            _createEventPropertyAccessorLabmdaMethod = typeof(EventAccessor).GetMethod("CreateEventPropertyAccessorLabmda", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private readonly DomainEventPropertyLocator _domainEventPropertyLocator;

        public EventAccessor(DomainEventPropertyLocator domainEventPropertyLocator)
        {
            _domainEventPropertyLocator = domainEventPropertyLocator;
        }

        public IEnumerable<EventProcessor> BuildEventProcessors(Type registeredEvent)
        {
            var eventProperties = _domainEventPropertyLocator.RetrieveDomainEventProperties(registeredEvent);
            return eventProperties.Select(eventProperty => new EventProcessor(registeredEvent, eventProperty, ProcessEventProperty(registeredEvent, eventProperty)));
        }

        private Action<object, Dictionary<string, object>> ProcessEventProperty(Type registeredEvent, PropertyInfo eventProperty)
        {
            var genericBuildLambdaMethod = _createEventPropertyAccessorLabmdaMethod.MakeGenericMethod(registeredEvent);

            var invoke = genericBuildLambdaMethod.Invoke(this, new object[] { eventProperty });

            var makeGenericMethod = _createToInternalStateCopyLambdaMethod.MakeGenericMethod(registeredEvent, eventProperty.PropertyType);
            var internalStateCopyLambda = makeGenericMethod.Invoke(this, new[] { eventProperty, invoke }) as Action<object, Dictionary<string, object>>;

            return internalStateCopyLambda;
        }

        protected Action<object, Dictionary<string, object>> CreateToInternalStateCopyLambda<TEventType, TPropertyType>(MemberInfo property, object func) where TEventType : class, IDomainEvent
        {
            return (eventType, internalState) =>
            {
                if (!internalState.ContainsKey(property.Name))
                    internalState.Add(property.Name, new object());

                internalState[property.Name] = ((Expression<Func<TEventType, TPropertyType>>)func).Compile().Invoke(eventType as TEventType);
            };
        }

        protected object CreateEventPropertyAccessorLabmda<TEventType>(MemberInfo property)
        {
            var expression = Expression.Parameter(typeof(TEventType), "x");
            return Expression.Lambda(Expression.MakeMemberAccess(expression, property), new[] { expression });
        }
    
    }
}