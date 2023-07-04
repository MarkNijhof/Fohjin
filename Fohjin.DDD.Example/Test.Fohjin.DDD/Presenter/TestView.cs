using Fohjin.DDD.BankApplication.Views;

namespace Test.Fohjin.DDD.Presenter;

public class TestView : ITestView
{
    public event EventAction OnTest = null!;

    public void Test()
    {
        OnTest();
    }

    // IView Interface plumbing
    public void Dispose()
    {
    }

    public DialogResults ShowDialog()
    {
        throw new NotImplementedException();
    }

    public void Close()
    {
    }
}