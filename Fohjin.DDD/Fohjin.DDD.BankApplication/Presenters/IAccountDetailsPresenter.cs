using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public interface IAccountDetailsPresenter : IPresenter
    {
        void SetAccount(Account account);
        void CloseTheAccount();
        void SaveAccountDetails();
        void PreformCashDeposite();
        void PreformCashWithdrawl();
        void FormElementGotChanged();
        void PreformTransfer();
    }
}