using System;
using System.Windows.Forms;
using Fohjin.DDD.BankApplication.Presenters;

namespace Fohjin.DDD.BankApplication.Views
{
    public interface IView<TPresenter> : IDisposable where TPresenter : IPresenter
    {
        DialogResult ShowDialog();
        void Close();
        void SetPresenter(TPresenter presenter);
    }
}