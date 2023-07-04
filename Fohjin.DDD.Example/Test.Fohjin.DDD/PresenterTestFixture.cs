using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;

namespace Test.Fohjin.DDD;

[TestClass]
public abstract class PresenterTestFixture<TPresenter>
{
    private IDictionary<Type, object> mocks = null!;

    protected TPresenter Presenter;
    protected Exception CaughtException;
    protected virtual void SetupDependencies() { }
    protected virtual void Given() { }
    protected abstract void When();
    protected virtual void Finally() { }

    [TestInitialize]
    public void Setup()
    {
        mocks = new Dictionary<Type, object>();
        CaughtException = new ThereWasNoExceptionButOneWasExpectedException();
        Presenter = BuildSubjectUnderTest();
        SetupDependencies();
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

    public MockDsl<TType> On<TType>() where TType : class
    {
        return new MockDsl<TType>(mocks);
    }

    public Mock<TType> OnDependency<TType>() where TType : class
    {
        return (Mock<TType>)mocks[typeof(TType)];
    }

    private TPresenter BuildSubjectUnderTest()
    {
        var constructorInfo = typeof(TPresenter).GetConstructors().First();

        foreach (var parameter in constructorInfo.GetParameters())
        {
            mocks.Add(parameter.ParameterType, CreateMock(parameter.ParameterType));
        }

        return (TPresenter)constructorInfo.Invoke(mocks.Values.Select(x => ((Mock)x).Object).ToArray());
    }

    private static object CreateMock(Type type)
    {
        var constructorInfo = typeof(Mock<>).MakeGenericType(type).GetConstructors().First();
        return constructorInfo.Invoke(new object[] { });
    }
}

public class MockDsl<TType> where TType : class
{
    private readonly IDictionary<Type, object> _mocks;

    public MockDsl(IDictionary<Type, object> mocks)
    {
        _mocks = mocks;
    }

    public ValueSetter<TType, TProperty> ValueFor<TProperty>(Expression<Func<TType, TProperty>> selector)
    {
        return new ValueSetter<TType, TProperty>(_mocks, selector);
    }

    public void FireEvent(Action<TType> fieldSelector)
    {
        if (!_mocks.ContainsKey(typeof(TType)))
            throw new Exception(string.Format("The requested dependency '{0}' is not specified in the constructor", typeof(TType).FullName));

        var mock = (Mock<TType>)_mocks[typeof(TType)];
        mock.Raise(fieldSelector);
    }

    public Verifier<TType> VerifyThat { get { return new Verifier<TType>(_mocks); } }
}

public class Verifier<TType> where TType : class
{
    private readonly IDictionary<Type, object> _mocks;

    public Verifier(IDictionary<Type, object> mocks)
    {
        _mocks = mocks;
    }

    public void ValueIsSetFor(Action<TType> selector)
    {
        if (!_mocks.ContainsKey(typeof(TType)))
            throw new Exception(string.Format("The requested dependency '{0}' is not specified in the constructor", typeof(TType).FullName));

        var mock = (Mock<TType>)_mocks[typeof(TType)];
        mock.VerifySet(selector);
    }

    public MethodVerifier<TType> Method(Expression<Action<TType>> selector)
    {
        return new MethodVerifier<TType>(_mocks, selector);
    }
}

public class MethodVerifier<TType> where TType : class
{
    private readonly IDictionary<Type, object> _mocks;
    private readonly Expression<Action<TType>> _fieldSelector;

    public MethodVerifier(IDictionary<Type, object> mocks, Expression<Action<TType>> fieldSelector)
    {
        _mocks = mocks;
        _fieldSelector = fieldSelector;
    }

    public void WasCalled()
    {
        if (!_mocks.ContainsKey(typeof(TType)))
            throw new Exception(string.Format("The requested dependency '{0}' is not specified in the constructor", typeof(TType).FullName));

        var mock = (Mock<TType>)_mocks[typeof(TType)];
        mock.Verify(_fieldSelector);
    }
}

public class ValueSetter<TType, TProperty> where TType : class
{
    private readonly IDictionary<Type, object> _mocks;
    private readonly Expression<Func<TType, TProperty>> _fieldSelector;

    public ValueSetter(IDictionary<Type, object> mocks, Expression<Func<TType, TProperty>> fieldSelector)
    {
        _mocks = mocks;
        _fieldSelector = fieldSelector;
    }

    public void IsSetTo(TProperty value)
    {
        if (!_mocks.ContainsKey(typeof(TType)))
            throw new Exception(string.Format("The requested dependency '{0}' is not specified in the constructor", typeof(TType).FullName));

        var mock = (Mock<TType>)_mocks[typeof(TType)];
        mock.SetupGet(_fieldSelector).Returns(value);
    }
}