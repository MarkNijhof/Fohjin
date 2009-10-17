using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Views
{
    public partial class AccountDetails : Form, IAccountDetailsView
    {
        private IAccountDetailsPresenter _presenter;

        public AccountDetails()
        {
            InitializeComponent();
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
        }

        public string AccountNameLabel
        {
            set { _accountNameLabel.Text = value; }
        }

        public string AccountNumberLabel
        {
            set { _accountNumberLabel.Text = value; }
        }

        public IEnumerable<Ledger> Ledgers
        {
            get { return (IEnumerable<Ledger>)_ledgers.DataSource; }
            set { _ledgers.DataSource = value; }
        }

        public IEnumerable<Account> TransferAccounts
        {
            get { return (IEnumerable<Account>)_transferAccounts.DataSource; }
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

        public Account GetSelectedTransferAccount()
        {
            return (Account)_transferAccounts.SelectedItem;
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

        public void SetPresenter(IAccountDetailsPresenter accountDetailsPresenter)
        {
            _presenter = accountDetailsPresenter;
        }

        private void _client_Changed(object sender, EventArgs e)
        {
            _presenter.FormElementGotChanged();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            _presenter.Cancel();
        }

        private void closeAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _presenter.CloseTheAccount();
        }

        private void changeAccountNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _presenter.InitiateAccountNameChange();
        }

        private void makeCashMutationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _presenter.InitiateMoneyDeposite();
        }

        private void makeCashWithdrawlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _presenter.InitiateMoneyWithdrawl();
        }

        private void transferMoneyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _presenter.InitiateMoneyTransfer();
        }

        private void _newAccountNameSaveButton_Click(object sender, EventArgs e)
        {
            _presenter.ChangeAccountName();
        }

        private void _transferButton_Click(object sender, EventArgs e)
        {
            _presenter.TransferMoney();
        }

        private void _withdrawlButton_Click(object sender, EventArgs e)
        {
            _presenter.WithdrawlMoney();
        }

        private void _depositeButton_Click(object sender, EventArgs e)
        {
            _presenter.DepositeMoney();
        }

        private void _amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = InputIsDecimal();
        }

        private bool InputIsDecimal()
        {
            decimal value;
            return
                Decimal.TryParse(_depositeAmount.Text, out value) &&
                Decimal.TryParse(_withdrawlAmount.Text, out value) &&
                Decimal.TryParse(_transferAmount.Text, out value);

        }
    }
}