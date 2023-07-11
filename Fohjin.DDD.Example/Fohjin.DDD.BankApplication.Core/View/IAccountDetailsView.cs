using Fohjin.DDD.Reporting.Dtos;

namespace Fohjin.DDD.BankApplication.Views
{
    public interface IAccountDetailsView : IView
    {
        string? AccountNameLabel { set; }
        string? AccountNumberLabel { set; }
        decimal BalanceLabel { set; }
        string? AccountName { get; set; }

        IEnumerable<LedgerReport>? Ledgers { get; set; }
        IEnumerable<AccountReport>? TransferAccounts { get; set; }
        AccountReport? GetSelectedTransferAccount();

        decimal DepositAmount { get; set; }
        decimal WithdrawalAmount { get; set; }
        decimal TransferAmount { get; set; }

        void EnableSaveButton();
        void DisableSaveButton();
        void EnableMenuButtons();
        void DisableMenuButtons();

        void EnableDetailsPanel();
        void EnableAccountNameChangePanel();
        void EnableDepositPanel();
        void EnableWithdrawalPanel();
        void EnableTransferPanel();

        event EventAction? OnCloseTheAccount;
        event EventAction? OnFormElementGotChanged;
        event EventAction? OnCancel;
        event EventAction? OnInitiateAccountNameChange;
        event EventAction? OnInitiateMoneyDeposit;
        event EventAction? OnInitiateMoneyWithdrawal;
        event EventAction? OnInitiateMoneyTransfer;
        event EventAction? OnChangeAccountName;
        event EventAction? OnDepositMoney;
        event EventAction? OnWithdrawalMoney;
        event EventAction? OnTransferMoney;
    }
}