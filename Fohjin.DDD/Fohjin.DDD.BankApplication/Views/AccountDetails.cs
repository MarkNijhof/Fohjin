using System;
using System.Collections.Generic;
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
        }

        public string AccountName
        {
            get { return _accountName.Text; }
            set { _accountName.Text = value; }
        }

        public string AccountNumber
        {
            get { return _accountNumber.Text; }
            set { _accountNumber.Text = value; }
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

        public void SetIsNewAccount()
        {
            CloseAccountButton.Enabled = false;
            groupBox1.Enabled = false;
            groupBox3.Enabled = false;
        }

        public void SetIsClosedAccount()
        {
            CloseAccountButton.Enabled = false;
            groupBox1.Enabled = false;
            groupBox3.Enabled = false;
        }

        public void SetIsExistingAccount()
        {
            CloseAccountButton.Enabled = true;
            groupBox1.Enabled = true;
            groupBox3.Enabled = true;
        }

        public void EnableSaveButton()
        {
            SaveAccountButton.Enabled = true;
        }

        public void DisableSaveButton()
        {
            SaveAccountButton.Enabled = false;
        }

        public void EnableWithdrawlButton()
        {
            WithdrawlButton.Enabled = true;
        }

        public void DisableWithdrawlButton()
        {
            WithdrawlButton.Enabled = false;
        }

        public void EnableDepositeButton()
        {
            DepositeButton.Enabled = true;
        }

        public void DisableDepositeButton()
        {
            DepositeButton.Enabled = false;
        }

        public void EnableTransferButton()
        {
            TransferButton.Enabled = true;
        }

        public void DisableTransferButton()
        {
            TransferButton.Enabled = false;
        }

        public Account GetSelectedTransferAccount()
        {
            return (Account)_transferAccounts.SelectedItem;
        }

        public decimal Amount
        {
            get { return string.IsNullOrEmpty(_amount.Text) ? 0 : Convert.ToDecimal(_amount.Text); }
            set { _amount.Text = value.ToString(); }
        }

        public decimal TransferAmount
        {
            get { return string.IsNullOrEmpty(_transferAmount.Text) ? 0 : Convert.ToDecimal(_transferAmount.Text); }
            set { _transferAmount.Text = value.ToString(); }
        }

        public decimal Balance
        {
            get { return string.IsNullOrEmpty(_balance.Text) ? 0 : Convert.ToDecimal(_balance.Text); }
            set { _balance.Text = value.ToString(); }
        }

        public void SetPresenter(IAccountDetailsPresenter accountDetailsPresenter)
        {
            _presenter = accountDetailsPresenter;
        }

        private void CloseAccountButton_Click(object sender, EventArgs e)
        {
            _presenter.CloseTheAccount();
        }

        private void SaveAccountButton_Click(object sender, EventArgs e)
        {
            _presenter.SaveAccountDetails();
        }

        private void DepositeButton_Click(object sender, EventArgs e)
        {
            _presenter.PreformCashDeposite();
        }

        private void WithdrawlButton_Click(object sender, EventArgs e)
        {
            _presenter.PreformCashWithdrawl();
        }

        private void _client_Changed(object sender, EventArgs e)
        {
            _presenter.FormElementGotChanged();
        }

        private void TransferButton_Click(object sender, EventArgs e)
        {
            _presenter.PreformTransfer();
        }
    }
}