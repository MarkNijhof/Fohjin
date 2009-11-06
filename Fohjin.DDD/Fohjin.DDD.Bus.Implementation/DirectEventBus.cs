using System.Collections.Generic;
using System.Reflection;
using Fohjin.DDD.EventStore.Bus;
using StructureMap;

namespace Fohjin.DDD.Bus.Implementation
{
    public class DirectEventBus : IEventBus
    {
        private readonly IContainer _container;
        private readonly MethodInfo _methodInfo;
        private readonly IQueue _inMemoryQueue;

        public DirectEventBus(IContainer container, IQueue inMemoryQueue)
        {
            _container = container;
            _methodInfo = GetType().GetMethod("DoPublish");
            _inMemoryQueue = inMemoryQueue;
            _inMemoryQueue.Pop(PublishEvent);
        }

        private void PublishEvent(object theEvent)
        {
            try
            {
                _methodInfo.MakeGenericMethod(theEvent.GetType()).Invoke(this, new[] { theEvent });
            }
            finally
            {
                _inMemoryQueue.Pop(PublishEvent);
            }
        }

        public void DoPublish<TMessage>(TMessage message) where TMessage : class
        {
            var eventHandlers = _container.GetAllInstances<IEventHandler<TMessage>>();
            foreach (var eventHandler in eventHandlers)
            {
                eventHandler.Execute(message);
            }
        }

        public void PublishMultiple<TMessage>(IEnumerable<TMessage> messages) where TMessage : class
        {
            foreach (var message in messages)
            {
                _inMemoryQueue.Put(message);
            }
        }
    }
}