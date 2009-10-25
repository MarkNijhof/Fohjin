using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace Test.Fohjin.DDD
{
    [Specification]
    public abstract class BaseTestFixture<TSubjectUnderTest>
    {
        private IDictionary<Type, object> mocks;

        protected TSubjectUnderTest SubjectUnderTest;
        protected Exception CaughtException;
        protected abstract void MockSetup();
        protected abstract void Given();
        protected abstract void When();

        [Given]
        public void Setup()
        {
            mocks = new Dictionary<Type, object>();
            CaughtException = new ThereWasNoExceptionButOneWasExpectedException();
            SubjectUnderTest = BuildSubjectUnderTest();
            MockSetup();

            try
            {
                Given();
                When();
            }
            catch (Exception exception)
            {
                CaughtException = exception;
            }
        }


        public Mock<TType> GetMock<TType>() where TType : class
        {
            return (Mock<TType>)mocks[typeof(TType)];
        }

        private TSubjectUnderTest BuildSubjectUnderTest()
        {
            var constructorInfo = typeof(TSubjectUnderTest).GetConstructors().First();

            foreach (var parameter in constructorInfo.GetParameters())
            {
                mocks.Add(parameter.ParameterType, CreateMock(parameter.ParameterType));
            }

            return (TSubjectUnderTest)constructorInfo.Invoke(mocks.Values.Select(x => ((Mock)x).Object).ToArray());
        }

        private static object CreateMock(Type type)
        {
            var constructorInfo = typeof(Mock<>).MakeGenericType(type).GetConstructors().First();
            return constructorInfo.Invoke(new object[] { });
        }
    }

    public class GivenAttribute : SetUpAttribute { }

    public class ThenAttribute : TestAttribute { }

    public class SpecificationAttribute : TestFixtureAttribute { }
}