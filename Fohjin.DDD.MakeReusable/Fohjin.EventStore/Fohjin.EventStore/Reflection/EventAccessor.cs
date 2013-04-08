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

        private readonly EventPropertyLocator _eventPropertyLocator;

        public EventAccessor(EventPropertyLocator eventPropertyLocator)
        {
            _eventPropertyLocator = eventPropertyLocator;
        }

        public IEnumerable<EventProcessor> BuildEventProcessors(Type registeredEvent)
        {
            return _eventPropertyLocator
                .RetrieveDomainEventProperties(registeredEvent)
                .Select(eventProperty => new EventProcessor(registeredEvent, eventProperty, ProcessEventProperty(registeredEvent, eventProperty)));
        }

        private Action<object, Dictionary<string, object>> ProcessEventProperty(Type registeredEvent, PropertyInfo eventProperty)
        {
            var eventPropertyAccessor = _createEventPropertyAccessorLabmdaMethod
                .MakeGenericMethod(registeredEvent)
                .Invoke(this, new object[] { eventProperty });

            return (Action<object, Dictionary<string, object>>)_createToInternalStateCopyLambdaMethod
                .MakeGenericMethod(registeredEvent, eventProperty.PropertyType)
                .Invoke(this, new[] { eventProperty, eventPropertyAccessor });
        }

        protected Action<object, Dictionary<string, object>> CreateToInternalStateCopyLambda<TEventType, TPropertyType>(MemberInfo property, Func<TEventType, TPropertyType> func) where TEventType : class, IDomainEvent
        {
            return (eventType, internalState) =>
            {
                if (!internalState.ContainsKey(property.Name))
                    internalState.Add(property.Name, null);

                internalState[property.Name] = func(eventType as TEventType);
            };
        }

        protected object CreateEventPropertyAccessorLabmda<TEventType>(MemberInfo property)
        {
            var expression = Expression.Parameter(typeof(TEventType), "x");
            return Expression
                .Lambda(Expression.MakeMemberAccess(expression, property), new[] { expression })
                .Compile();
        }
    
    }
}