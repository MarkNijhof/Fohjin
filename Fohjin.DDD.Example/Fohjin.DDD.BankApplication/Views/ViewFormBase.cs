namespace Fohjin.DDD.BankApplication.Views
{
    public abstract class ViewFormBase : Form, IView
    {
        DialogResults IView.ShowDialog()
        {
            return (DialogResults)(int)ShowDialog();
        }
    }
}
