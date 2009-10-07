using System.Windows.Forms;
using Fohjin.DDD.BankApplication.Presenters;

namespace Fohjin.DDD.BankApplication.Views
{
    public interface IView<TPresenter> where TPresenter : IPresenter
    {
        DialogResult ShowDialog();
        void SetPresenter(TPresenter presenter);
    }
}