using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Domain;
using Fohjin.DDD.Domain.Entities.Mementos;
using Fohjin.DDD.Events;
using Fohjin.EventStorage;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace Test.Fohjin.DDD.Domain
{
    [Specification]
    public abstract class CommandHandlerTestFixture<TCommandHandler, TAggregateRoot> 
        where TCommandHandler : ICommandHandler
        where TAggregateRoot : IOrginator, IEventProvider, new()
    {
        protected TAggregateRoot aggregateRoot;
        protected TCommandHandler commandHandler;
        protected Exception caught;
        protected IEnumerable<IDomainEvent> events;

        protected abstract IEnumerable<IDomainEvent> Given();
        protected abstract void When();

        [SetUp]
        public void Setup()
        {
            caught = null;
            aggregateRoot = new TAggregateRoot();
            aggregateRoot.LoadFromHistory(Given());

            commandHandler = BuildCommandHandler();

            try
            {
                When();
                events = aggregateRoot.GetChanges();
            }
            catch (Exception e)
            {
                caught = e;
            }
        }

        private TCommandHandler BuildCommandHandler()
        {
            var constructorInfo = typeof (TCommandHandler).GetConstructors().First();

            IContainer container = new Container();

            foreach (var parameter in constructorInfo.GetParameters())
            {
                if (parameter.ParameterType == typeof(IRepository))
                {
                    var repositoryMock = new Mock<IRepository>();
                    repositoryMock.Setup(x => x.GetById<TAggregateRoot>(It.IsAny<Guid>())).Returns(aggregateRoot);
                    repositoryMock.Setup(x => x.Save(It.IsAny<TAggregateRoot>())).Callback<TAggregateRoot>(x => aggregateRoot = x);
                    container.Inject(parameter.ParameterType, repositoryMock.Object);
                    continue;
                }

                container.Inject(parameter.ParameterType, CreateMock(parameter.ParameterType));
            }

            return container.GetInstance<TCommandHandler>();
        }

        private static object CreateMock(Type type)
        {
            var constructorInfo = typeof (Mock<>).MakeGenericType(type).GetConstructors().First();
            return constructorInfo.Invoke(new object[]{});
        }
    }
}