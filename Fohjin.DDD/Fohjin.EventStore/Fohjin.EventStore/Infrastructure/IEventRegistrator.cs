using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Fohjin.EventStore.Infrastructure
{
    public interface IEventRegistrator
    {
        Dictionary<Type, List<Action<object, Dictionary<string, object>>>> RegisterEventHandlers(Type hostType, object proxy);
    }

    public class EventRegistrator : IEventRegistrator
    {
        private static readonly MethodInfo _createToInternalStateCopyLambdaMethod;
        private static readonly MethodInfo _createEventPropertyAccessorLabmdaMethod;

        static EventRegistrator()
        {
            _createToInternalStateCopyLambdaMethod = typeof(EventRegistrator).GetMethod("CreateToInternalStateCopyLambda", BindingFlags.Instance | BindingFlags.NonPublic);
            _createEventPropertyAccessorLabmdaMethod = typeof(EventRegistrator).GetMethod("CreateEventPropertyAccessorLabmda", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private readonly Dictionary<Type, List<Action<object, Dictionary<string, object>>>> _registeredEventHandlers;

        public EventRegistrator()
        {
            _registeredEventHandlers = new Dictionary<Type, List<Action<object, Dictionary<string, object>>>>();
        }

        public Dictionary<Type, List<Action<object, Dictionary<string, object>>>> RegisterEventHandlers(Type hostType, object proxy)
        {
            return ProcessRegisteredEvents(GetRegisteredEvents(hostType, proxy));
        }

        private static IEnumerable<Type> GetRegisteredEvents(IReflect proxyType, object proxy)
        {
            return (IEnumerable<Type>)proxyType
                .GetMethod("RegisteredEvents", BindingFlags.Static | BindingFlags.NonPublic)
                .Invoke(null, new object[] { });
        }

        private Dictionary<Type, List<Action<object, Dictionary<string, object>>>> ProcessRegisteredEvents(IEnumerable<Type> registeredEvents)
        {
            var interfaceProperties = typeof(IDomainEvent).GetProperties().Select(x => x.Name);

            foreach (var registeredEvent in registeredEvents)
            {
                var eventProperties = registeredEvent
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(x => !interfaceProperties.Contains(x.Name))
                    .ToList();

                if (eventProperties.Count() == 0)
                    continue;

                ProcessEventProperties(registeredEvent, eventProperties);
            }
            return _registeredEventHandlers;
        }

        private void ProcessEventProperties(Type registeredEvent, IEnumerable<PropertyInfo> eventProperties)
        {
            _registeredEventHandlers.Add(registeredEvent, new List<Action<object, Dictionary<string, object>>>());
            var genericBuildLambdaMethod = _createEventPropertyAccessorLabmdaMethod.MakeGenericMethod(registeredEvent);

            foreach (var eventProperty in eventProperties)
            {
                var invoke = genericBuildLambdaMethod.Invoke(this, new object[] { eventProperty });

                var makeGenericMethod = _createToInternalStateCopyLambdaMethod.MakeGenericMethod(registeredEvent, eventProperty.PropertyType);
                var internalStateCopyLambda = makeGenericMethod.Invoke(this, new[] { eventProperty, invoke }) as Action<object, Dictionary<string, object>>;

                _registeredEventHandlers[registeredEvent].Add(internalStateCopyLambda);
            }
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