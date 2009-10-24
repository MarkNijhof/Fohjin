using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Views
{
    public partial class AccountDetails : Form, IAccountDetailsView
    {
        public AccountDetails()
        {
            InitializeComponent();
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            RegisterCLientEvents();
        }

        public event Action OnCloseTheAccount;
        public event Action OnFormElementGotChanged;
        public event Action OnCancel;
        public event Action OnInitiateAccountNameChange;
        public event Action OnInitiateMoneyDeposite;
        public event Action OnInitiateMoneyWithdrawl;
        public event Action OnInitiateMoneyTransfer;
        public event Action OnChangeAccountName;
        public event Action OnDepositeMoney;
        public event Action OnWithdrawlMoney;
        public event Action OnTransferMoney;
        public event Action OnRefresh;

        private void RegisterCLientEvents()
        {
            changeAccountNameToolStripMenuItem.Click += (e, s) => OnInitiateAccountNameChange();
            closeAccountToolStripMenuItem.Click += (e, s) => OnCloseTheAccount();
            makeCashMutationToolStripMenuItem.Click += (e, s) => OnInitiateMoneyDeposite();
            makeCashWithdrawlToolStripMenuItem.Click += (e, s) => OnInitiateMoneyWithdrawl();
            transferMoneyToolStripMenuItem.Click += (e, s) => OnInitiateMoneyTransfer();
            _depositeCancelButton.Click += (e, s) => OnCancel();
            _depositeButton.Click += (e, s) => OnDepositeMoney();
            _withdrawlCancelButton.Click += (e, s) => OnCancel();
            _withdrawlButton.Click += (e, s) => OnWithdrawlMoney();
            _transferCancelButton.Click += (e, s) => OnCancel();
            _transferButton.Click += (e, s) => OnTransferMoney();
            _newAccountNameCancelButton.Click += (e, s) => OnCancel();
            _newAccountNameSaveButton.Click += (e, s) => OnChangeAccountName();
            _refreshButton.Click += (e, s) => OnRefresh();
        }

        public string AccountNameLabel
        {
            set { _accountNameLabel.Text = value; }
        }

        public string AccountNumberLabel
        {
            set { _accountNumberLabel.Text = value; }
        }

        public IEnumerable<LedgerReport> Ledgers
        {
            get { return (IEnumerable<LedgerReport>)_ledgers.DataSource; }
            set { _ledgers.DataSource = value; }
        }

        public IEnumerable<AccountReport> TransferAccounts
        {
            get { return (IEnumerable<AccountReport>)_transferAccounts.DataSource; }
            set { _transferAccounts.DataSource = value; }
        }

        public void EnableDetailsPanel()
        {
            tabControl1.SelectedIndex = 0;
        }

        public void EnableDepositePanel()
        {
            tabControl1.SelectedIndex = 1;
        }

        public void EnableWithdrawlPanel()
        {
            tabControl1.SelectedIndex = 2;
        }

        public void EnableTransferPanel()
        {
            tabControl1.SelectedIndex = 3;
        }

        public void EnableAccountNameChangePanel()
        {
            tabControl1.SelectedIndex = 4;
        }

        public void EnableSaveButton()
        {
            _depositeButton.Enabled = true;
            _withdrawlButton.Enabled = true;
            _transferButton.Enabled = true;
            _newAccountNameSaveButton.Enabled = true;
        }

        public void DisableSaveButton()
        {
            _depositeButton.Enabled = false;
            _withdrawlButton.Enabled = false;
            _transferButton.Enabled = false;
            _newAccountNameSaveButton.Enabled = false;
        }

        public void EnableMenuButtons()
        {
            changeAccountNameToolStripMenuItem.Enabled = true;
            closeAccountToolStripMenuItem.Enabled = true;
            makeCashWithdrawlToolStripMenuItem.Enabled = true;
            makeCashMutationToolStripMenuItem.Enabled = true;
            transferMoneyToolStripMenuItem.Enabled = true;
        }

        public void DisableMenuButtons()
        {
            changeAccountNameToolStripMenuItem.Enabled = false;
            closeAccountToolStripMenuItem.Enabled = false;
            makeCashWithdrawlToolStripMenuItem.Enabled = false;
            makeCashMutationToolStripMenuItem.Enabled = false;
            transferMoneyToolStripMenuItem.Enabled = false;
        }

        public AccountReport GetSelectedTransferAccount()
        {
            return (AccountReport)_transferAccounts.SelectedItem;
        }

        public decimal DepositeAmount
        {
            get { return _depositeAmount.Text.Trim() == "," ? 0 : Convert.ToDecimal(_depositeAmount.Text.Trim()); }
            set { _depositeAmount.Text = value.ToString(); }
        }

        public decimal WithdrawlAmount
        {
            get { return _withdrawlAmount.Text.Trim() == "," ? 0 : Convert.ToDecimal(_withdrawlAmount.Text.Trim()); }
            set { _withdrawlAmount.Text = value.ToString(); }
        }

        public decimal TransferAmount
        {
            get { return _transferAmount.Text.Trim() == "," ? 0 : Convert.ToDecimal(_transferAmount.Text.Trim()); }
            set { _transferAmount.Text = value.ToString(); }
        }

        public decimal BalanceLabel
        {
            set { _balanceLabel.Text = value.ToString(); }
        }

        public string AccountName
        {
            get { return _accountName.Text; }
            set { _accountName.Text = value; }
        }

        private void _amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            decimal value;
            e.Handled = !Decimal.TryParse(e.KeyChar.ToString(), out value);
        }

        private void _depositeAmount_TextChanged(object sender, EventArgs e)
        {
            if (OnFormElementGotChanged != null)
               OnFormElementGotChanged();
        }
    }
}