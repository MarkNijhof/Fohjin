using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        public IEnumerable<LedgerDto> Ledgers
        {
            get { return (IEnumerable<LedgerDto>)_ledgers.DataSource; }
            set { _ledgers.DataSource = value; }
        }

        public string Amount
        {
            get { return _amount.Text; }
            set { _amount.Text = value; }
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
            _presenter.InitiateDeposite();
        }

        private void WithdrawlButton_Click(object sender, EventArgs e)
        {
            _presenter.InitiateWithdrawl();
        }
    }
}