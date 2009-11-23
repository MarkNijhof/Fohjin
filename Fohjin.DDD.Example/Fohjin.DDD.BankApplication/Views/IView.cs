using System;
using System.Windows.Forms;

namespace Fohjin.DDD.BankApplication.Views
{
    public interface IView : IDisposable
    {
        DialogResult ShowDialog();
        void Close();
    }
}