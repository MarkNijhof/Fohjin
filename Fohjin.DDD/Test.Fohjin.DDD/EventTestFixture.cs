using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.EventHandlers;
using Moq;
using NUnit.Framework;

namespace Test.Fohjin.DDD
{
    [Specification]
    public abstract class EventTestFixture<TEvent, TEventHandler>
        where TEvent : class, IMessage
        where TEventHandler : class, IEventHandler<TEvent>
    {
        private IDictionary<Type, object> mocks;
        protected object update;
        protected object insert;
        protected Exception caught;
        protected IEventHandler<TEvent> eventHandler;

        protected abstract void MockSetup();
        protected abstract TEvent When();

        [SetUp]
        public void Setup()
        {
            mocks = new Dictionary<Type, object>();
            caught = new ThereWasNoExceptionButOneWasExpectedException();
            update = null;
            insert = null;

            eventHandler = BuildCommandHandler();

            MockSetup();
            try
            {
                eventHandler.Execute(When());
            }
            catch (Exception e)
            {
                caught = e;
            }
        }
        public Mock<TType> GetMock<TType>() where TType : class
        {
            if (!mocks.ContainsKey(typeof(TType)))
                throw new Exception(string.Format("The event handler '{0}' does not have a dependency upon '{1}'", typeof(TEventHandler).FullName, typeof(TType).FullName));

            return (Mock<TType>)mocks[typeof(TType)];
        }

        private IEventHandler<TEvent> BuildCommandHandler()
        {
            var constructorInfo = typeof(TEventHandler).GetConstructors().First();

            foreach (var parameter in constructorInfo.GetParameters())
            {
                if (parameter.ParameterType == typeof(IReportingRepository))
                {
                    var repositoryMock = new Mock<IReportingRepository>();
                    mocks.Add(parameter.ParameterType, repositoryMock);
                    continue;
                }

                mocks.Add(parameter.ParameterType, CreateMock(parameter.ParameterType));
            }

            return (IEventHandler<TEvent>)constructorInfo.Invoke(mocks.Values.Select(x => ((Mock)x).Object).ToArray());
        }

        private static object CreateMock(Type type)
        {
            var constructorInfo = typeof(Mock<>).MakeGenericType(type).GetConstructors().First();
            return constructorInfo.Invoke(new object[] { });
        }
    }
}