using System.Collections.Generic;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Views
{
    public interface IAccountDetailsView : IView<IAccountDetailsPresenter>
    {
        string AccountNameLabel { set; }
        string AccountNumberLabel { set; }
        decimal BalanceLabel { set; }
        string AccountName { get; set; }

        IEnumerable<Ledger> Ledgers { get; set; }
        IEnumerable<Account> TransferAccounts { get; set; }
        Account GetSelectedTransferAccount();

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
    }
}