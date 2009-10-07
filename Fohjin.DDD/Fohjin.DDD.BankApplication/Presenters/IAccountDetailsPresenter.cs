using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public interface IAccountDetailsPresenter : IPresenter
    {
        void SetAccount(AccountDto accountDto);
        void CloseTheAccount();
        void SaveAccountDetails();
        void InitiateDeposite();
        void InitiateWithdrawl();
    }
}