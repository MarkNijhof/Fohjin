using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Views
{
    public partial class ClientDetails : Form, IClientDetailsView
    {
        private IClientDetailsPresenter _presenter;

        public ClientDetails()
        {
            InitializeComponent();
        }

        private void AddNewAccountButton_Click(object sender, EventArgs e)
        {
            _presenter.CreateNewAccountAndAttachToClient();
        }

        private void SaveClientButton_Click(object sender, EventArgs e)
        {
            _presenter.SaveClientChanges();
        }

        public string ClientName
        {
            get { return _clientName.Text; }
            set { _clientName.Text = value; }
        }

        public string Street
        {
            get { return _street.Text; }
            set { _street.Text = value; }
        }

        public string StreetNumber
        {
            get { return _streetNumber.Text; }
            set { _streetNumber.Text = value; }
        }

        public string PostalCode
        {
            get { return _postalCode.Text; }
            set { _postalCode.Text = value; }
        }

        public string City
        {
            get { return _city.Text; }
            set { _city.Text = value; }
        }

        public IEnumerable<Account> Accounts
        {
            get { return (IEnumerable<Account>)_accounts.DataSource; }
            set { _accounts.DataSource = value; }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber.Text; }
            set { _phoneNumber.Text = value; }
        }

        public void SetPresenter(IClientDetailsPresenter clientDetailsPresenter)
        {
            _presenter = clientDetailsPresenter;
        }

        public Account GetSelectedAccount()
        {
            return (Account)_accounts.SelectedItem;
        }

        private void _accounts_DoubleClick(object sender, EventArgs e)
        {
            _presenter.OpenSelectedAccount();
        }
    }
}