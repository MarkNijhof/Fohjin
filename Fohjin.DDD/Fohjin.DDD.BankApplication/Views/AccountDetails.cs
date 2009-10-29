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

        public event EventAction OnCloseTheAccount;
        public event EventAction OnFormElementGotChanged;
        public event EventAction OnCancel;
        public event EventAction OnInitiateAccountNameChange;
        public event EventAction OnInitiateMoneyDeposite;
        public event EventAction OnInitiateMoneyWithdrawl;
        public event EventAction OnInitiateMoneyTransfer;
        public event EventAction OnChangeAccountName;
        public event EventAction OnDepositeMoney;
        public event EventAction OnWithdrawlMoney;
        public event EventAction OnTransferMoney;

        private void RegisterCLientEvents()
        {
            changeAccountNameToolStripMenuItem.Click += (s, e) => OnInitiateAccountNameChange();
            closeAccountToolStripMenuItem.Click += (s, e) => OnCloseTheAccount();
            makeCashMutationToolStripMenuItem.Click += (s, e) => OnInitiateMoneyDeposite();
            makeCashWithdrawlToolStripMenuItem.Click += (s, e) => OnInitiateMoneyWithdrawl();
            transferMoneyToolStripMenuItem.Click += (s, e) => OnInitiateMoneyTransfer();
            _depositeCancelButton.Click += (s, e) => OnCancel();
            _depositeButton.Click += (s, e) => OnDepositeMoney();
            _withdrawlCancelButton.Click += (s, e) => OnCancel();
            _withdrawlButton.Click += (s, e) => OnWithdrawlMoney();
            _transferCancelButton.Click += (s, e) => OnCancel();
            _transferButton.Click += (s, e) => OnTransferMoney();
            _newAccountNameCancelButton.Click += (s, e) => OnCancel();
            _newAccountNameSaveButton.Click += (s, e) => OnChangeAccountName();
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
            _depositeAmount.Focus();
        }

        public void EnableWithdrawlPanel()
        {
            tabControl1.SelectedIndex = 2;
            _withdrawlAmount.Focus();
        }

        public void EnableTransferPanel()
        {
            tabControl1.SelectedIndex = 3;
            _transferAmount.Focus();
        }

        public void EnableAccountNameChangePanel()
        {
            tabControl1.SelectedIndex = 4;
            _accountName.Focus();
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