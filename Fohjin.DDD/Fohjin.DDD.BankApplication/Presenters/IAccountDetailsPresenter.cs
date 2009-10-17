using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public interface IAccountDetailsPresenter : IPresenter
    {
        void SetAccount(Account account);
        void CloseTheAccount();
        void FormElementGotChanged();

        void Cancel();
        
        void InitiateAccountNameChange();
        void InitiateMoneyDeposite();
        void InitiateMoneyWithdrawl();
        void InitiateMoneyTransfer();

        void ChangeAccountName();
        void DepositeMoney();
        void WithdrawlMoney();
        void TransferMoney();
    }
}