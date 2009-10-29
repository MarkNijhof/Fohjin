using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public interface IAccountDetailsPresenter : IPresenter
    {
        void SetAccount(AccountReport accountReport);
    }
}