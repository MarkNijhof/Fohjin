using Fohjin.DDD.Reporting.Dtos;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public interface IAccountDetailsPresenter : IPresenter
    {
        void SetAccount(AccountReport? accountReport);
    }
}