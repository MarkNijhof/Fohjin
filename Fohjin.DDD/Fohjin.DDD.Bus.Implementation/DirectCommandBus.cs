using System.Collections.Generic;
using System.Reflection;
using Fohjin.DDD.CommandHandlers;
using StructureMap;

namespace Fohjin.DDD.Bus.Implementation
{
    public class DirectCommandBus : ICommandBus
    {
        private readonly IContainer _container;
        private readonly MethodInfo _methodInfo;

        public DirectCommandBus(IContainer container)
        {
            _container = container;
            _methodInfo = GetType().GetMethod("Publish");
        }

        public void Publish<TMessage>(TMessage message) where TMessage : class, IMessage
        {
            var eventHandlers = _container.GetAllInstances<ICommandHandler<TMessage>>();
            foreach (var eventHandler in eventHandlers)
            {
                eventHandler.Execute(message);
            }
        }

        public void PublishMultiple<TMessage>(IEnumerable<TMessage> messages) where TMessage : class, IMessage
        {
            foreach (var message in messages)
            {
                _methodInfo.MakeGenericMethod(message.GetType()).Invoke(this, new object[] { message });
            }
        }
    }
}