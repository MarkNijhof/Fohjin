using Fohjin.DDD.BankApplication.Views;

namespace Test.Fohjin.DDD.Presenter
{
    public interface ITestView : IView
    {
        event EventAction OnTest;
    }
}