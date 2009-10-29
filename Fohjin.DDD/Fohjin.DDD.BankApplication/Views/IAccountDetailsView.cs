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

        event EventAction OnCloseTheAccount;
        event EventAction OnFormElementGotChanged;

        event EventAction OnCancel;

        event EventAction OnInitiateAccountNameChange;
        event EventAction OnInitiateMoneyDeposite;
        event EventAction OnInitiateMoneyWithdrawl;
        event EventAction OnInitiateMoneyTransfer;

        event EventAction OnChangeAccountName;
        event EventAction OnDepositeMoney;
        event EventAction OnWithdrawlMoney;
        event EventAction OnTransferMoney;
    }
}