using System.Collections.Generic;
using System.Reflection;
using Fohjin.DDD.EventHandlers;
using StructureMap;

namespace Fohjin.DDD.Bus.Implementation
{
    public class DirectEventBus : IEventBus
    {
        private readonly IContainer _container;
        private readonly MethodInfo _methodInfo;

        public DirectEventBus(IContainer container)
        {
            _container = container;
            _methodInfo = GetType().GetMethod("Publish");
        }

        public void Publish<TMessage>(TMessage message) where TMessage : class, IMessage
        {
            var eventHandlers = _container.GetAllInstances<IEventHandler<TMessage>>();
            foreach (var eventHandler in eventHandlers)
            {
                eventHandler.Execute(message);
            }
        }

        public void PublishMultiple<TMessage>(IEnumerable<TMessage> messages) where TMessage : class, IMessage
        {
            foreach (var message in messages)
            {
                _methodInfo.MakeGenericMethod(message.GetType()).Invoke(this, new object[]{ message });
            }
        }
    }
}