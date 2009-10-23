using System;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public interface IPopupPresenter : IPresenter
    {
        void CatchPossibleException(Action action);
    }
}