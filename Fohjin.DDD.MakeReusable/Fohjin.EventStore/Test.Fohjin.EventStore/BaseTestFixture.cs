using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace Test.Fohjin.EventStore
{
    [Specification]
    public abstract class BaseTestFixture
    {
        protected Exception CaughtException;
        protected virtual void Given() { }
        protected abstract void When();
        protected virtual void Finally() { }

        [Given]
        public void Setup()
        {
            CaughtException = new NoExceptionWasThrownException();
            Given();

            try
            {
                When();
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
    }

    [Specification]
    public abstract class BaseTestFixture<TSubjectUnderTest>
    {
        private Dictionary<Type, object> mocks;

        protected Dictionary<Type, object> ActualImplementation;
        protected TSubjectUnderTest SubjectUnderTest;
        protected Exception CaughtException;

        protected virtual void SetupDependencies() { }
        protected virtual void Given() { }
        protected abstract void When();
        protected virtual void Finally() { }

        [Given]
        public void Setup()
        {
            mocks = new Dictionary<Type, object>();
            ActualImplementation = new Dictionary<Type, object>();
            CaughtException = new NoExceptionWasThrownException();

            BuildMocks();
            SetupDependencies();
            BuildSubjectUnderTest();

            Given();

            try
            {
                When();
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

        public Mock<TType> On<TType>() where TType : class
        {
            return (Mock<TType>)mocks[typeof(TType)];
        }

        private void BuildMocks()
        {
            var constructorInfo = typeof(TSubjectUnderTest).GetConstructors().First();

            foreach (var parameter in constructorInfo.GetParameters())
            {
                mocks.Add(parameter.ParameterType, CreateMock(parameter.ParameterType));
            }
        }

        private static object CreateMock(Type type)
        {
            var constructorInfo = typeof(Mock<>).MakeGenericType(type).GetConstructors().First();
            return constructorInfo.Invoke(new object[] { });
        }

        private void BuildSubjectUnderTest()
        {
            var constructorInfo = typeof(TSubjectUnderTest).GetConstructors().First();

            var parameters = new List<object>();
            foreach (var mock in mocks)
            {
                object theObject;
                if (!ActualImplementation.TryGetValue(mock.Key, out theObject))
                    theObject = ((Mock)mock.Value).Object;

                parameters.Add(theObject);
            }

            SubjectUnderTest = (TSubjectUnderTest)constructorInfo.Invoke(parameters.ToArray());
        }
    }

    public class NoExceptionWasThrownException : Exception { }

    public class GivenAttribute : SetUpAttribute { }

    public class ThenAttribute : TestAttribute { }

    public class SpecificationAttribute : TestFixtureAttribute { }
}