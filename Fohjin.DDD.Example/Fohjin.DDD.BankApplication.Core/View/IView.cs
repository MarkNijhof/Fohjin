namespace Fohjin.DDD.BankApplication.Views
{
    public interface IView : IDisposable
    {
        DialogResults ShowDialog();
        void Close();
    }
}