namespace Fohjin.DDD.BankApplication.Views
{
    public interface IPopupView : IView
    {
        string Exception { set; }
        string Message { set; }
    }
}