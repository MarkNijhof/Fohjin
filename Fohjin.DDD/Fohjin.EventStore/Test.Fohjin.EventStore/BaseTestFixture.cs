using System;
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

    public class NoExceptionWasThrownException : Exception { }

    public class GivenAttribute : SetUpAttribute { }

    public class ThenAttribute : TestAttribute { }

    public class SpecificationAttribute : TestFixtureAttribute { }
}