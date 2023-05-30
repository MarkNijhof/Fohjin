namespace Fohjin.DDD.BankApplication.Views
{
    public interface IView : IDisposable
    {
        DialogResult ShowDialog();
        void Close();
    }
}