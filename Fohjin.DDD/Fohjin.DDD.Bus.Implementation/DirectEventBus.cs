using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Fohjin.DDD.EventStore.Bus;
using StructureMap;

namespace Fohjin.DDD.Bus.Implementation
{
    public class DirectEventBus : IEventBus, IDisposable
    {
        private bool _keepProcessing;
        private readonly IContainer _container;
        private readonly MethodInfo _methodInfo;
        private readonly IQueue _inMemoryQueue;
        private readonly Thread _queueProcessor;

        public DirectEventBus(IContainer container, IQueue inMemoryQueue)
        {
            _keepProcessing = true;
            _container = container;
            _methodInfo = GetType().GetMethod("DoPublish");
            _inMemoryQueue = inMemoryQueue;
            _queueProcessor = new Thread(ProcessQueue);
            _queueProcessor.Start();
        }

        private void ProcessQueue()
        {
            _inMemoryQueue.Pop(PublishEvent);

            while (_keepProcessing)
            {
                Thread.Sleep(500);
            }
        }

        private void PublishEvent(object theEvent)
        {
            _methodInfo.MakeGenericMethod(theEvent.GetType()).Invoke(this, new [] { theEvent });

            _inMemoryQueue.Pop(PublishEvent);
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

        public void Dispose()
        {
            _keepProcessing = false;
        }
    }
}