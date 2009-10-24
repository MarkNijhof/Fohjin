using System.Collections.Generic;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Views
{
    public interface IAccountDetailsView : IView
    {
        string AccountNameLabel { set; }
        string AccountNumberLabel { set; }
        decimal BalanceLabel { set; }
        string AccountName { get; set; }

        IEnumerable<LedgerReport> Ledgers { get; set; }
        IEnumerable<AccountReport> TransferAccounts { get; set; }
        AccountReport GetSelectedTransferAccount();

        decimal DepositeAmount { get; set; }
        decimal WithdrawlAmount { get; set; }
        decimal TransferAmount { get; set; }

        void EnableSaveButton();
        void DisableSaveButton();
        void EnableMenuButtons();
        void DisableMenuButtons();

        void EnableDetailsPanel();
        void EnableAccountNameChangePanel();
        void EnableDepositePanel();
        void EnableWithdrawlPanel();
        void EnableTransferPanel();

        event Action OnCloseTheAccount;
        event Action OnFormElementGotChanged;

        event Action OnCancel;

        event Action OnInitiateAccountNameChange;
        event Action OnInitiateMoneyDeposite;
        event Action OnInitiateMoneyWithdrawl;
        event Action OnInitiateMoneyTransfer;

        event Action OnChangeAccountName;
        event Action OnDepositeMoney;
        event Action OnWithdrawlMoney;
        event Action OnTransferMoney;
        event Action OnRefresh;
    }
}