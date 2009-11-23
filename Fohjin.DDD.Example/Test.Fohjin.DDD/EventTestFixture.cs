using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Reporting;
using Moq;

namespace Test.Fohjin.DDD
{
    [Specification]
    public abstract class EventTestFixture<TEvent, TEventHandler>
        where TEvent : class
        where TEventHandler : class, IEventHandler<TEvent>
    {
        private IDictionary<Type, object> mocks;

        protected Exception CaughtException;
        protected IEventHandler<TEvent> EventHandler;
        protected virtual void SetupDependencies() { }
        protected abstract TEvent When();
        protected virtual void Finally() { }

        [Given]
        public void Setup()
        {
            mocks = new Dictionary<Type, object>();
            CaughtException = new ThereWasNoExceptionButOneWasExpectedException();
            EventHandler = BuildCommandHandler();
            SetupDependencies();

            try
            {
                EventHandler.Execute(When());
            }
            catch (Exception exception)
            {
                CaughtException = exception;
            }
            finally
            {
                Finally();
            }
        }

        public Mock<TType> OnDependency<TType>() where TType : class
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