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

        public void SetIsNewClient()
        {
            addNewAccountToolStripMenuItem.Enabled = false;
        }

        public void SetIsExistingClient()
        {
            addNewAccountToolStripMenuItem.Enabled = true;
        }

        public void EnableSaveButton()
        {
            createToolStripMenuItem.Enabled = true;
        }

        public void DisableSaveButton()
        {
            createToolStripMenuItem.Enabled = false;
        }

        private void _accounts_DoubleClick(object sender, EventArgs e)
        {
            _presenter.OpenSelectedAccount();
        }

        private void _client_Changed(object sender, EventArgs e)
        {
            _presenter.FormElementGotChanged();
        }

        private void addNewAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _presenter.CreateNewAccountAndAttachToClient();
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _presenter.SaveClientChanges();
        }
    }
}